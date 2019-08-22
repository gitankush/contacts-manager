using Evolent.ContactsMgmt.Common.Contracts;
using Evolent.ContactsMgmt.DataSource;
using Evolent.ContactsMgmt.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolent.ContactsMgmt.BL.Repository
{
    public class ContactRepository : IRepository<ContactDTO>, IDisposable
    {
        private DbSet<Contact> _dbSet;

        private ContactsManagerDBEntities _dbContext;

        private ContactDTO MapDBEntityToDTO(Contact contact)
        {
            ContactDTO contactDTO = new ContactDTO();

            if (contact != null)
            {
                contactDTO.ContactID = contact.ContactID;
                contactDTO.FirstName = contact.FirstName;
                contactDTO.LastName = contact.LastName;
                contactDTO.Email = contact.Email;
                contactDTO.PhoneNumber = contact.PhoneNumber;
                contactDTO.Status = contact.Status;
            }
            return contactDTO;
        }

        private Contact MapDTOToDBEntity(ContactDTO contactDTO,Contact contact=null)
        {
            if (contact == null)
            {
                contact = new Contact();
            }
            if (contactDTO != null)
            {
                contact.ContactID = contactDTO.ContactID;
                contact.FirstName = contactDTO.FirstName;
                contact.LastName = contactDTO.LastName;
                contact.Email = contactDTO.Email;
                contact.PhoneNumber = contactDTO.PhoneNumber;
                contact.Status = contactDTO.Status;
            }
            return contact;
        }

        public ContactRepository()
        {
            _dbContext = new ContactsManagerDBEntities();
            _dbSet = _dbContext.Set<Contact>();
        }
        public bool IsDataAlreadyExist(ContactDTO contactDTO)
        {
            return _dbContext.Contacts.Any(c => c.Status==true && (c.Email.Equals(contactDTO.Email, StringComparison.InvariantCultureIgnoreCase) || c.PhoneNumber.Equals(contactDTO.PhoneNumber, StringComparison.InvariantCultureIgnoreCase)));
        }

        public void Add(ContactDTO contactDTO)
        {
            _dbSet.Add(MapDTOToDBEntity(contactDTO));
        }

        public void Delete(int contactID)
        {
            var contact = _dbContext.Contacts.FirstOrDefault(c => c.ContactID == contactID);
            if (contact != null)
            {
                if (_dbContext.Entry(contact).State == EntityState.Detached)
                {
                    _dbSet.Attach(contact);
                }
                _dbSet.Remove(contact);
            }
        }
        public void Delete(string email)
        {
            var contact = _dbContext.Contacts.FirstOrDefault(c => c.Email.Equals(email,StringComparison.InvariantCultureIgnoreCase));
            if (contact != null)
            {
                if (_dbContext.Entry(contact).State == EntityState.Detached)
                {
                    _dbSet.Attach(contact);
                }
                _dbSet.Remove(contact);
            }
        }
        public void Update(ContactDTO contactDTO)
        {
            var oldContact = _dbContext.Contacts.FirstOrDefault(c => c.ContactID==contactDTO.ContactID);
            if (oldContact != null)
            {
                var contact = MapDTOToDBEntity(contactDTO, oldContact);
                _dbSet.Attach(contact);
                _dbContext.Entry(contact).State = EntityState.Modified;
            }
        }
        public ContactDTO Get(int contactID)
        {
            var contact =_dbSet.Where(c=>c.ContactID==contactID).FirstOrDefault();
            return MapDBEntityToDTO(contact);
        }
        public IEnumerable<ContactDTO> GetAll()
        {
            IEnumerable<Contact> contacts = _dbSet.ToList();
            List<ContactDTO> contactsDTOs = new List<ContactDTO>();

            foreach (var contact in contacts)
            {
                contactsDTOs.Add(MapDBEntityToDTO(contact));
            }
            return contactsDTOs.AsEnumerable();
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbSet = null;
            _dbContext.Dispose();
        }

    }
}
