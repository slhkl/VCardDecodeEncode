using Data.Models;
using System.Net;
using System.Text;

namespace Business.Helpers
{
    public static class Extension
    {
        const string NewLine = "\r\n";
        const string Separator = ";";
        const string Header = "BEGIN:VCARD\r\nVERSION:3.0";
        const string Name = "N:";
        const string FormattedName = "FN:";
        const string OrganizationName = "ORG:";
        const string TitlePrefix = "TITLE:";
        const string PhotoPrefix = "PHOTO;ENCODING=b;TYPE=JPEG:";
        const string PhonePrefix = "TEL;type=";
        const string PhoneSubPrefix = ",VOICE:";
        const string AddressPrefix = "ADR;type=";
        const string AddressSubPrefix = ":;;";
        const string EmailPrefix = "EMAIL;type=";
        const string WebSitePrefix = "item1.X-ABLabel:";
        const string WebSite = "item1.URL:";
        const string Footer = "END:VCARD";
        const string TwoDots = ":";

        public static string CreateVCard(this Contact contact)
        {
            StringBuilder fw = new StringBuilder();
            fw.Append(Header);
            fw.Append(NewLine);

            //Full Name
            if (!string.IsNullOrEmpty(contact.FirstName) || !string.IsNullOrEmpty(contact.LastName))
            {
                fw.Append(Name);
                fw.Append(contact.LastName);
                fw.Append(Separator);
                fw.Append(contact.FirstName);
                fw.Append(Separator);
                fw.Append(NewLine);
            }

            //Formatted Name
            if (!string.IsNullOrEmpty(contact.FormattedName))
            {
                fw.Append(FormattedName);
                fw.Append(contact.FormattedName);
                fw.Append(NewLine);
            }

            //Organization name
            if (!string.IsNullOrEmpty(contact.Organization))
            {
                fw.Append(OrganizationName);
                fw.Append(contact.Organization);
                if (!string.IsNullOrEmpty(contact.OrganizationPosition))
                {
                    fw.Append(Separator);
                    fw.Append(contact.OrganizationPosition);
                }
                fw.Append(NewLine);
            }

            //Title
            if (!string.IsNullOrEmpty(contact.Title))
            {
                fw.Append(TitlePrefix);
                fw.Append(contact.Title);
                fw.Append(NewLine);
            }

            if (!string.IsNullOrEmpty(contact.WebSite))
            {
                fw.Append(WebSite);
                fw.Append(contact.WebSite);
                if (!string.IsNullOrEmpty(contact.WebSiteTitle))
                {
                    fw.Append(NewLine);
                    fw.Append(WebSitePrefix);
                    fw.Append(contact.WebSiteTitle);
                    fw.Append(NewLine);
                }
            }

            //Photo
            if (!string.IsNullOrEmpty(contact.Photo))
            {
                fw.Append(PhotoPrefix);
                fw.Append(contact.Photo);
                fw.Append(NewLine);
                fw.Append(NewLine);
            }

            //Phones
            foreach (var item in contact.Phones)
            {
                fw.Append(PhonePrefix);
                fw.Append(item.Type);
                fw.Append(PhoneSubPrefix);
                fw.Append(item.Number);
                fw.Append(NewLine);
            }

            //Addresses
            foreach (var item in contact.Addresses ?? new List<Address>())
            {
                fw.Append(AddressPrefix);
                fw.Append(item.Type);
                fw.Append(AddressSubPrefix);
                fw.Append(item.Description);
                fw.Append(NewLine);
            }

            //Email
            foreach (var item in contact.Email)
            {
                fw.Append(EmailPrefix);
                fw.Append(item.Type);
                fw.Append(TwoDots);
                fw.Append(item.Address);
                fw.Append(NewLine);
            }

            fw.Append(Footer);

            return fw.ToString();
        }
    }
}
