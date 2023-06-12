using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppNotiz
{
    public class Notiz
    {
        public string id;
        public string erstelldatum;
        public string titel = "";
        public string notiz = "";

        public Notiz()
        {

        }
        public Notiz(string id,string erstelldatum,string titel, string notiz)
        {
            this.id = id;
            this.erstelldatum = erstelldatum;
            this.titel = titel;
            this.notiz = notiz;
        }

        public string Titel
        {
            get { return this.titel; }
            set { this.titel = value; }
        }
        public string Notiz_
        {
            get { return this.notiz; }
            set { this.notiz = value; }
        }
        public string Erstelldatum
        {
            get { return this.erstelldatum; }
            set { this.erstelldatum = value; }
        }
        public string Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
    }
}
