using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolent.ContactsMgmt.DataSource.Repository
{
    public class ContactRepository : IRepository<Contact>, IDisposable
    {
        private DbSet<Contact> _dbSet;

        private ContactsManagerDBEntities _dbContext;

        public ContactRepository()
        {
            _dbContext = new ContactsManagerDBEntities();
            _dbSet = _dbContext.Set<Contact>();
        }
        public void Add(Contact contact)
        {
            var isContactExistInDB = _dbContext.Contacts.Any(c => c.Email.Equals(contact.Email,StringComparison.InvariantCultureIgnoreCase) || c.PhoneNumber.Equals(contact.PhoneNumber, StringComparison.InvariantCultureIgnoreCase));
            if (isContactExistInDB)
            {
                Update(contact);
            }
            else
            {
                _dbSet.Add(contact);
            }
        }

        public void Delete(Contact contact)
        {
           var isContactExistInDB = _dbContext.Contacts.Any(c => c.ContactID==contact.ContactID);
            if (isContactExistInDB)
            {
                if (_dbContext.Entry(contact).State == EntityState.Detached)
                {
                    _dbSet.Attach(contact);
                }
                _dbSet.Remove(contact);
            }
        }

        public Contact Get(int contactID)
        {
            Contact contact = new Contact();
            var matchedContact= _dbContext.Contacts.FirstOrDefault(c => c.ContactID == contactID);
            if (matchedContact != null)
            {
                contact = matchedContact;
            }
            return contact;
        }

        public void Update(Contact contact)
        {
            if (contact.ContactID==0)
            {


            }
            Contact contact= _dbContext.Contacts.Any(c => c.Email.Equals(contact.Email, StringComparison.InvariantCultureIgnoreCase) || c.PhoneNumber.Equals(contact.PhoneNumber, StringComparison.InvariantCultureIgnoreCase));
            if (party != null)
            {
                _dbSet.Attach(MapPOCOEntityToDBObject(sender, party));
                _dbContext.Entry(party).State = EntityState.Modified;
            }
        }

        public IEnumerable<Contact> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }


        public void Dispose()
        {
            _dbSet = null;
            _dbContext.Dispose();
        }
    }
}
