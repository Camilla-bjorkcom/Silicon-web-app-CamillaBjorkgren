//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace Web_API_Camilla.Controllers;

//[Route("api/[controller]")]
//[ApiController]
//[UseApiKey]
//[Authorize]
//public class SubscribersController : ControllerBase
//{
//    // private readonly repository 

//    [HttpPost]
//    [UseApiKey]
//    public async Task<IActionResult> CreateAsync(subscriber dto)
//    {
//        if (ModelState.IsValid)
//        {
//            if (!await _context.Subscribers.AnyAsync())// find by email.
//            {
//                try
//                {
//                    //var subcsriberEntity = new SubscriberEntity { Email = email };
//                    // add. repository
//                    // await savechangesasync

//                    //return Created("",null)

//                    //https://youtu.be/wRLWDB7gZF4?t=1102
//                }



//                catch
//                {
//                    return Problem("Unable to create subscription.");
//                }

//            }
//            return Conflict("Your email address is already subscribed");

//        }
//        return BadRequest();

//    }

//    [HttpGet]
//    public async Task<IActionResult> GetAllAsync()
//    {
//        var subscribers = await _context.Subscribers.ToListAsync();
//        if (subscribers.Any())
//        {
//            return Ok(subscribers);
//        }
//        return NotFound();
//    }

//    [HttpGet("{email}")]
//    public async Task<IActionResult> GetOneAsync(string email)
//    {
//        var subscriber = await _context.Subscribers.FirstOrDefaultAsync(x => x.Email == email);
//        if (subscriber != null)
//        {
//            return Ok(subscriber);
//        }
//        return NotFound();
//    }

//    [HttpPut("{id}")]
//    public async Task<IActionResult> UpdateOne(int id, string email)
//    {
//        var subscriber = await _context.Subscribers.FirstOrDefaultAsync(x => x.Id == id);
//        if (subscriber != null)
//        {
//            subscriber.Email = email;
//            _context.Subscribers.Update(subscriber);
//            await _context.SaveChangesaAsync();

//            return Ok(subscriber);
//        }
//        return Ok();
//    }

//    [HttpDelete("{id}")]
//    public async Task<IActionResult> DeleteAsync(int id)
//    {
//        var subscriber = await _context.Subscribers.FirstOrDefaultAsync(x => x.Id == id);
//        if (subscriber != null)
//        {
//            _context.Subscriber.Delete(subscriber);
//            await _context.SaveChangesAsync();
//            return Ok();
//        }
//        return NotFound();
//    }

//}
