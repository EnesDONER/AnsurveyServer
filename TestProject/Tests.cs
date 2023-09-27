using Business.Abstract;
using Business.Concrete;
using Business.ThirdPartyServices.MessageBrokerServices;
using Business.ThirdPartyServices.MessageBrokerServices.RabbitMQ;
using Business.ThirdPartyServices.PaymentServices;
using Business.ThirdPartyServices.PaymentServices.IyziPay;
using Business.ThirdPartyServices.PaymentServices.PayPal;
using Business.ThirdPartyServices.StorageServices;
using Business.ThirdPartyServices.StorageServices.Azure;
using Business.ThirdPartyServices.StorageServices.Local;
using Castle.Core.Configuration;
using Core.DataAccess.MongoOptions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.MongoDB;
using Entities.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace TestProject
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        //[Test]
        //public void PayInstance()
        //{
        //    int userId = 17;
        //    int cardId = 1;
        //    decimal amount = 1;
   
        //    IUserService userService = new UserManager(new EfUserDal());
        //    ICardService cardService = new CardManager(new EfCardDal());


        //    IThirdPartyPaymentService thirdPartyPaymentService = new PaypalAdapter();
        //    IPaymentService paymentManager = new PaymentManager(userService, cardService, thirdPartyPaymentService);

        //    // �deme i�lemi ger�ekle�tirin
        //    var paymentResult = paymentManager.Pay(userId, cardId, amount);

        //    if (paymentResult.Success)
        //    {
        //        Console.WriteLine(paymentResult.Message);
        //        if (paymentResult.Message == "Paypal")
        //        {
        //            Assert.Pass();

        //        }
        //        Console.WriteLine("Iyzico �al�st�");
        //        Assert.Fail();
        //    }
        //    else
        //    {
        //        Console.WriteLine("�deme ba�ar�s�z: " + paymentResult.Message);
        //        Assert.Fail();
        //    }

          
        //}

        [Test]
        public void StorageInstange()
        {
            var mongoSettings = Options.Create(new MongoSettings
            {
                ConnectionString = "your_connection_string_here",
                Database = "your_database_name_here"
                // Di�er ayarlar...
            });
            IAdDal adDal = new MAdDal(mongoSettings);
            IWatchedAdDal watchedAdDal = new MWatchedAdDal(mongoSettings);
            IAdFilterDal adFilterDal = new MAdFilterDal(mongoSettings);
            IAdFilterService adFilterService = new AdFilterManager(adFilterDal);
            IUserService userService = new UserManager(new EfUserDal());
            IMessageBrokerService<EmailDto> messageBrokerService = new RabbitMQAdapter<EmailDto>();
          
            IStorageService storageService = new LocalStorage();
            
            // AdManager s�n�f�n� olu�turun ve ba��ml�l�klar� enjekte edin

            IAdService adService = new AdManager(adDal, watchedAdDal, adFilterService, userService, messageBrokerService, storageService);
            var result =  adService.Upload("test", "12", null);
            Console.WriteLine(result);
             Assert.Fail();
        }
    }
}