using Microsoft.AspNetCore.Mvc;
using AdminAPI.Models;
using AdminAPI.Models.DataManager;
using Microsoft.EntityFrameworkCore;

namespace AdminAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly CustomerManager _repo;

    public CustomersController(CustomerManager repo)
    {
        _repo = repo;
    }

    // GET: api/customers
    [HttpGet]
    public IEnumerable<Customer> Get()
    {
        return _repo.GetAll();
    }



    // GET: api/customers/{id}
    [HttpGet("{id}")]
    public ActionResult<Customer> Get(int id)
    {
        return _repo.Get(id);
    }


    // PUT api/movies
    [HttpPut]
    public void Put([FromBody] Customer customer)
    {
        _repo.Update(customer.CustomerID, customer);
    }




    // PUT api/customers/{id}/lock
    [HttpPut("{id}/lock")]
    public void Lock(int id)
    {
        Console.WriteLine(id);
        _repo.Lock(id);
    }

    // PUT api/customers/{id}/unlock
    [HttpPut("{id}/unlock")]
    public void Unlock(int id)
    {
        _repo.Unlock(id);
    }






}