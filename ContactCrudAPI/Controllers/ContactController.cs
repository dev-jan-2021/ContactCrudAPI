using ContactCrudAPI.Models;
using Microsoft.AspNetCore.Mvc;
using ContactCrudAPI.Services;

namespace ContactManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly JsonFileService _jsonFileService;

        public ContactsController(JsonFileService jsonFileService)
        {
            _jsonFileService = jsonFileService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Contact>> GetContacts()
        {
            return _jsonFileService.GetAllContacts();
        }

        [HttpPost]
        public IActionResult AddContact(Contact contact)
        {
            var contacts = _jsonFileService.GetAllContacts();
            contact.Id = contacts.Count > 0 ? contacts.Max(c => c.Id) + 1 : 1;
            contacts.Add(contact);
            _jsonFileService.SaveContacts(contacts);
            return CreatedAtAction(nameof(GetContacts), new { id = contact.Id }, contact);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateContact(int id, Contact contact)
        {
            var contacts = _jsonFileService.GetAllContacts();
            var index = contacts.FindIndex(c => c.Id == id);
            if (index < 0) return NotFound();

            contacts[index] = contact;
            _jsonFileService.SaveContacts(contacts);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteContact(int id)
        {
            var contacts = _jsonFileService.GetAllContacts();
            var contactToRemove = contacts.FirstOrDefault(c => c.Id == id);
            if (contactToRemove == null) return NotFound();

            contacts.Remove(contactToRemove);
            _jsonFileService.SaveContacts(contacts);
            return NoContent();
        }
    }
}
