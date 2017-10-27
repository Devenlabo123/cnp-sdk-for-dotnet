﻿using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Cnp.Sdk;

namespace Cnp.Sdk.Test.Certification
{
    [TestFixture]
    class TestCert5Token
    {
        private CnpOnline cnp;

        [TestFixtureSetUp]
        public void setUp()
        {
            Dictionary<string, string> config = new Dictionary<string, string>();
            config.Add("url", "https://payments.vantivprelive.com/vap/communicator/online");
            config.Add("reportGroup", "Default Report Group");
            config.Add("username", "SDKTEAM");
            config.Add("version", "8.13");
            config.Add("timeout", "500");
            config.Add("merchantId", "1288791");
            config.Add("password", "V3r5K6v7");
            config.Add("printxml", "true");
            config.Add("logFile", null);
            config.Add("neuterAccountNums", null);
            config.Add("proxyHost", Properties.Settings.Default.proxyHost);
            config.Add("proxyPort", Properties.Settings.Default.proxyPort);
            cnp = new CnpOnline(config);
        }

        [Test]
        public void test50()
        {
            registerTokenRequestType request = new registerTokenRequestType();
            request.id = "1";
            request.orderId = "50";
            request.accountNumber = "4457119922390123";

            registerTokenResponse response = cnp.RegisterToken(request);
            Assert.AreEqual("445711", response.bin);
            Assert.AreEqual(methodOfPaymentTypeEnum.VI, response.type);
            Assert.AreEqual("801", response.response);
            Assert.AreEqual("1111222233330123", response.cnpToken);
            Assert.AreEqual("Account number was successfully registered", response.message);
        }

        [Test]
        public void test51()
        {
            registerTokenRequestType request = new registerTokenRequestType();
            request.id = "1";
            request.orderId = "51";
            request.accountNumber = "4457119999999999";

            registerTokenResponse response = cnp.RegisterToken(request);
            Assert.AreEqual("820", response.response);
            Assert.AreEqual("Credit card number was invalid", response.message);
        }

        [Test]
        public void test52()
        {
            registerTokenRequestType request = new registerTokenRequestType();
            request.id = "1";
            request.orderId = "52";
            request.accountNumber = "4457119922390123";

            registerTokenResponse response = cnp.RegisterToken(request);
            Assert.AreEqual("445711", response.bin);
            Assert.AreEqual(methodOfPaymentTypeEnum.VI, response.type);
            Assert.AreEqual("802", response.response);
            Assert.AreEqual("1111222233330123", response.cnpToken);
            Assert.AreEqual("Account number was previously registered", response.message);
        }

        [Test]
        public void test53()
        {
            registerTokenRequestType request = new registerTokenRequestType();
            request.id = "1";
            request.orderId = "53";
            echeckForTokenType echeck = new echeckForTokenType();
            echeck.accNum = "1099999998";
            echeck.routingNum = "114567895";
            request.echeckForToken = echeck; ;

            registerTokenResponse response = cnp.RegisterToken(request);
            Assert.AreEqual(methodOfPaymentTypeEnum.EC, response.type);
            Assert.AreEqual("998", response.eCheckAccountSuffix);
            Assert.AreEqual("801", response.response);
            Assert.AreEqual("Account number was successfully registered", response.message);
            Assert.AreEqual("111922223333000998", response.cnpToken);
        }

        [Test]
        public void test54()
        {
            registerTokenRequestType request = new registerTokenRequestType();
            request.id = "1";
            request.orderId = "54";
            echeckForTokenType echeck = new echeckForTokenType();
            echeck.accNum = "1022222102";
            echeck.routingNum = "1145_7895";
            request.echeckForToken = echeck; ;

            registerTokenResponse response = cnp.RegisterToken(request);
            Assert.AreEqual("900", response.response);
        }

        [Test]
        public void test55()
        {
            authorization auth = new authorization();
            auth.id = "1";
            auth.orderId = "55";
            auth.amount = 15000;
            auth.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.number = "5435101234510196";
            card.expDate = "1112";
            card.cardValidationNum = "987";
            card.type = methodOfPaymentTypeEnum.MC;
            auth.card = card;

            authorizationResponse response = cnp.Authorize(auth);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual("801", response.tokenResponse.tokenResponseCode);
            Assert.AreEqual("Account number was successfully registered", response.tokenResponse.tokenMessage);
            Assert.AreEqual(methodOfPaymentTypeEnum.MC, response.tokenResponse.type);
            Assert.AreEqual("543510", response.tokenResponse.bin);
        }

        [Test]
        public void test56()
        {
            authorization auth = new authorization();
            auth.id = "1";
            auth.orderId = "56";
            auth.amount = 15000;
            auth.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.number = "5435109999999999";
            card.expDate = "1112";
            card.cardValidationNum = "987";
            card.type = methodOfPaymentTypeEnum.MC;
            auth.card = card;

            authorizationResponse response = cnp.Authorize(auth);
            Assert.AreEqual("301", response.response);
        }

        [Test]
        public void test57()
        {
            authorization auth = new authorization();
            auth.id = "1";
            auth.orderId = "57";
            auth.amount = 15000;
            auth.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.number = "5435101234510196";
            card.expDate = "1112";
            card.cardValidationNum = "987";
            card.type = methodOfPaymentTypeEnum.MC;
            auth.card = card;

            authorizationResponse response = cnp.Authorize(auth);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual("802", response.tokenResponse.tokenResponseCode);
            Assert.AreEqual("Account number was previously registered", response.tokenResponse.tokenMessage);
            Assert.AreEqual(methodOfPaymentTypeEnum.MC, response.tokenResponse.type);
            Assert.AreEqual("543510", response.tokenResponse.bin);
        }

        [Test]
        public void test59()
        {
            authorization auth = new authorization();
            auth.id = "1";
            auth.orderId = "59";
            auth.amount = 15000;
            auth.orderSource = orderSourceType.ecommerce;
            cardTokenType token = new cardTokenType();
            token.cnpToken = "1712990000040196";
            token.expDate = "1112";
            auth.token = token;

            authorizationResponse response = cnp.Authorize(auth);
            Assert.AreEqual("822", response.response);
            Assert.AreEqual("Token was not found", response.message);
        }

        [Test]
        public void test60()
        {
            authorization auth = new authorization();
            auth.id = "1";
            auth.orderId = "60";
            auth.amount = 15000;
            auth.orderSource = orderSourceType.ecommerce;
            cardTokenType token = new cardTokenType();
            token.cnpToken = "1712999999999999";
            token.expDate = "1112";
            auth.token = token;

            authorizationResponse response = cnp.Authorize(auth);
            Assert.AreEqual("823", response.response);
        }

        [Test]
        public void test61()
        {
            echeckSale sale = new echeckSale();
            sale.id = "1";
            sale.orderId = "61";
            sale.amount = 15000;
            sale.orderSource = orderSourceType.ecommerce;
            contact billToAddress = new contact();
            billToAddress.firstName = "Tom";
            billToAddress.lastName = "Black";
            sale.billToAddress = billToAddress;
            echeckType echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking; ;
            echeck.accNum = "1099999003";
            echeck.routingNum = "114567895";
            sale.echeck = echeck;

            echeckSalesResponse response = cnp.EcheckSale(sale);
            Assert.AreEqual("801", response.tokenResponse.tokenResponseCode);
            Assert.AreEqual("Account number was successfully registered", response.tokenResponse.tokenMessage);
            Assert.AreEqual(methodOfPaymentTypeEnum.EC, response.tokenResponse.type);
            Assert.AreEqual("111922223333444003", response.tokenResponse.cnpToken);
        }

        [Test]
        public void test62()
        {
            echeckSale sale = new echeckSale();
            sale.id = "1";
            sale.orderId = "62";
            sale.amount = 15000;
            sale.orderSource = orderSourceType.ecommerce;
            contact billToAddress = new contact();
            billToAddress.firstName = "Tom";
            billToAddress.lastName = "Black";
            sale.billToAddress = billToAddress;
            echeckType echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking; ;
            echeck.accNum = "1099999999";
            echeck.routingNum = "114567895";
            sale.echeck = echeck;

            echeckSalesResponse response = cnp.EcheckSale(sale);
            Assert.AreEqual("801", response.tokenResponse.tokenResponseCode);
            Assert.AreEqual("Account number was successfully registered", response.tokenResponse.tokenMessage);
            Assert.AreEqual(methodOfPaymentTypeEnum.EC, response.tokenResponse.type);
            Assert.AreEqual("999", response.tokenResponse.eCheckAccountSuffix);
            Assert.AreEqual("111922223333444999", response.tokenResponse.cnpToken);
        }

        [Test]
        public void test63()
        {
            echeckSale sale = new echeckSale();
            sale.id = "1";
            sale.orderId = "63";
            sale.amount = 15000;
            sale.orderSource = orderSourceType.ecommerce;
            contact billToAddress = new contact();
            billToAddress.firstName = "Tom";
            billToAddress.lastName = "Black";
            sale.billToAddress = billToAddress;
            echeckType echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking; ;
            echeck.accNum = "1099999999";
            echeck.routingNum = "214567892";
            sale.echeck = echeck;

            echeckSalesResponse response = cnp.EcheckSale(sale);
            Assert.AreEqual("801", response.tokenResponse.tokenResponseCode);
            Assert.AreEqual("Account number was successfully registered", response.tokenResponse.tokenMessage);
            Assert.AreEqual(methodOfPaymentTypeEnum.EC, response.tokenResponse.type);
            Assert.AreEqual("999", response.tokenResponse.eCheckAccountSuffix);
            Assert.AreEqual("111922223333555999", response.tokenResponse.cnpToken);
        }
            
    }
}
