﻿using Assignment2.Data;
using Assignment2.Filter;
using Assignment2.Models;
using Assignment2.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Principal;

namespace Assignment2.Controllers
{
    [AuthorizeCustomer]
    public class BillPayController : Controller
    {
        private readonly McbaContext _context;
        private int AccountNumber => HttpContext.Session.GetInt32("AccountNumber").Value;

        public BillPayController(McbaContext context) => _context = context;


        public IActionResult Index(int id)
        {
            ViewBag.Id = id;
            return View(_context.BillPay.Where(x => x.AccountNumber == id).OrderByDescending(x => x.ScheduleTimeUtc).ToList());
        }

        public IActionResult Add() => View();

        [HttpPost]
        public async Task<IActionResult> Add(TransactionViewModel Bpay)
        {
            ModelState.Clear();


            var account = await _context.Accounts.FindAsync(AccountNumber);
            if (account.AccountType == "Savings" && (account.Balance - Bpay.Amount) <= 0)
                ModelState.AddModelError("InvalidFunds", "Not Enough Funds");

            if (account.AccountType == "Checking" && (account.Balance - Bpay.Amount) <= 300)
                ModelState.AddModelError("InvalidFunds", "Funds cannot be under $300");

            if (!_context.Payee.Any(x => x.PayeeID == Bpay.DestinationAccountNumber))
                ModelState.AddModelError("DestinationAccountNumber", "PayeeID doesn't exist");
            if (Bpay.Amount.HasMoreThanTwoDecimalPlaces())
                ModelState.AddModelError(nameof(Bpay.Amount), "Amount cannot have more than 2 decimal places.");
            if (DateTime.Now > Bpay.DateTime)
                ModelState.AddModelError(nameof(DateTime), "Cannot schedule for time in the past");

            if (!ModelState.IsValid)
                return View(Bpay);

            Bpay.AccountNumber = AccountNumber;
            Bpay.TransactionType = "BillPay";

            return RedirectToAction("ConfirmTransaction", "Transaction", Bpay);


        }

        public async Task<IActionResult> Cancel(int id) => View(await _context.BillPay.FindAsync(id));

        public async Task<IActionResult> ConfirmCancel(int id)
        {
            var BillPay = _context.BillPay.Remove(await _context.BillPay.FindAsync(id));
            _context.SaveChanges();
            return RedirectToAction("Index", "BillPay", new { id = AccountNumber });
        }

    }
}
