using MicroserviceTrainings.Eventbus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTrainings.Services.BasketService
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository repository;
        private readonly IIdentityService identityService;
        private readonly IEventBus _eventBus;
        private readonly ILogger<BasketController> _logger;

        // Constructor, gerekli servisleri enjekte eder
        public BasketController(
            ILogger<BasketController> logger,
            IBasketRepository repository,
            IIdentityService identityService,
            IEventBus eventBus)
        {
            _logger = logger;
            this.repository = repository;
            this.identityService = identityService;
            _eventBus = eventBus;
        }

        // GET api/basket
        [HttpGet]
        public IActionResult Get()
        {
            // Servisin çalışır durumda olduğunu belirten bir mesaj döndürür
            return Ok("Basket Service is Up and Running");
        }

        // GET api/basket/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerBasket), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CustomerBasket>> GetBasketByIdAsync(string id)
        {
            // Belirli bir kullanıcıya ait sepet bilgilerini getirir
            var basket = await repository.GetBasketAsync(id);

            // Eğer sepet bulunamazsa, yeni bir sepet nesnesi oluşturulur
            return Ok(basket ?? new CustomerBasket(id));
        }

        // POST api/basket/update
        [HttpPost]
        [Route("update")]
        [ProducesResponseType(typeof(CustomerBasket), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CustomerBasket>> UpdateBasketAsync([FromBody] CustomerBasket value)
        {
            // Sepeti günceller ve güncellenmiş sepeti döndürür
            return Ok(await repository.UpdateBasketAsync(value));
        }

        // POST api/basket/additem
        [Route("additem")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [HttpPost]
        public async Task<ActionResult> AddItemToBasket([FromBody] BasketItem basketItem)
        {
            // Kullanıcıya ait sepete yeni bir öğe ekler
            var userId = identityService.GetUserName().ToString();
            var basket = await repository.GetBasketAsync(userId);

            // Eğer sepet bulunamazsa, yeni bir sepet oluşturulur
            if (basket == null)
            {
                basket = new CustomerBasket(userId);
            }

            basket.Items.Add(basketItem);

            await repository.UpdateBasketAsync(basket);

            return Ok();
        }

        // POST api/basket/checkout
        [Route("checkout")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> CheckoutAsync([FromBody] BasketCheckout basketCheckout)
        {
            // Ödeme işlemini gerçekleştirir ve bir sipariş oluşturur
            var userId = basketCheckout.Buyer;
            var basket = await repository.GetBasketAsync(userId);

            if (basket == null)
            {
                return BadRequest();
            }

            var userName = identityService.GetUserName();

            var eventMessage = new OrderCreatedIntegrationEvent(userId, userName, basketCheckout.City, basketCheckout.Street,
                basketCheckout.State, basketCheckout.Country, basketCheckout.ZipCode, basketCheckout.CardNumber, basketCheckout.CardHolderName,
                basketCheckout.CardExpiration, basketCheckout.CardSecurityNumber, basketCheckout.CardTypeId, basketCheckout.Buyer, basket);

            try
            {
                // Sipariş oluşturma olayını yayınlama
                _eventBus.Publish(eventMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Publishing integration event: {IntegrationEventId} from {BasketService.App}", eventMessage.Id);
                throw;
            }

            return Accepted();
        }

        // DELETE api/basket/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task DeleteBasketByIdAsync(string id)
        {
            // Belirli bir sepeti siler
            await repository.DeleteBasketAsync(id);
        }
    }

}
