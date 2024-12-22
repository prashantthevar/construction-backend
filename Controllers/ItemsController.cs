using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MyApiApp.Models;
using MyApiApp.Services;

namespace MyApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IMongoCollection<Item> _itemsCollection;

        public ItemsController(MongoDbService mongoDbService)
        {
            if (mongoDbService == null)
            {
                throw new ArgumentNullException(nameof(mongoDbService), "MongoDbService cannot be null.");
            }

            // Initialize the collection (ensure the collection name matches your MongoDB setup)
            _itemsCollection = mongoDbService.GetCollection<Item>("items"); 
        }

        // GET: api/items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            try
            {
                var items = await _itemsCollection.Find(Builders<Item>.Filter.Empty).ToListAsync();
                return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/items/{id}
        [HttpGet("{id:length(24)}")] // MongoDB ObjectId length validation
        public async Task<ActionResult<Item>> GetItem(string id)
        {
            try
            {
                var item = await _itemsCollection.Find(i => i.Id == id).FirstOrDefaultAsync();

                if (item == null)
                {
                    return NotFound($"Item with ID {id} was not found.");
                }

                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/items
        [HttpPost]
        public async Task<ActionResult<Item>> PostItem([FromBody] Item item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("Item cannot be null.");
                }

                await _itemsCollection.InsertOneAsync(item);

                return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
