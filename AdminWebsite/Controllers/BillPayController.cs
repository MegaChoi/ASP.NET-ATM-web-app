﻿using System.Text;
using Microsoft.AspNetCore.Mvc;
using AdminWebsite.Models;
using Newtonsoft.Json;
using AdminWebsite.Filter;

namespace AdminWebsite.Controllers
{
    [AuthorizeAdmin]

    public class BillPayController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private HttpClient Client => _clientFactory.CreateClient("api");

        public BillPayController(IHttpClientFactory clientFactory) => _clientFactory = clientFactory;

        public async Task<IActionResult> Index(int id)
        {
            var response = await Client.GetAsync($"api/billpay/{id}");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            var result = await response.Content.ReadAsStringAsync();

            var accounts = JsonConvert.DeserializeObject<List<Account>>(result);

            return View(accounts);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var response = await Client.GetAsync($"api/billpay/{id}/transactions");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            var result = await response.Content.ReadAsStringAsync();

            var billpayments = JsonConvert.DeserializeObject<List<BillPay>>(result);

            return View(billpayments);
        }




        [HttpGet]
        public async Task<IActionResult> Block(int id, int accountid)
        {
            var response = await Client.PutAsync($"api/billpay/{id}/block", null);
            if (!response.IsSuccessStatusCode)
            {
                // Log the error message for debugging purposes
                Console.WriteLine("Error: " + response.StatusCode);
                throw new Exception();
            };
            return RedirectToAction("Edit", new {id = accountid });
        }

        [HttpGet]
        public async Task<IActionResult> Unblock(int id, int accountid)
        {
            var response = await Client.PutAsync($"/api/billpay/{id}/unblock", null);

            if (!response.IsSuccessStatusCode)
            {
                // Log the error message for debugging purposes
                Console.WriteLine("Error: " + response.StatusCode);
                throw new Exception();
            }

            return RedirectToAction("Edit", new { id = accountid });
        }


    }
}