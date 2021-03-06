﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//DOT_HELPERS

namespace DOT.NET.OwnHelpers
{
    public static class UrlHelpers
    {
        public static string IkonyKategoriSciezka(this UrlHelper helper, string nazwaIkonyKategorii)
        {
            var IkonyKategoriFolder = AppConfig.IkonyKategoriiFolderWzgledny;
            var sciezka = Path.Combine(IkonyKategoriFolder, nazwaIkonyKategorii);
            var sciezkaBezwzgledna = helper.Content(sciezka);
            return sciezkaBezwzgledna;
        }

        public static string ObrazekSciezka(this UrlHelper helper, string nazwaObrazka)
        {
            var obrazkiFolder = AppConfig.ObrazkiFolderWzgledny;
            var sciezka = Path.Combine(obrazkiFolder, nazwaObrazka);
            var sciezkaBezwzgledna = helper.Content(sciezka);
            return sciezkaBezwzgledna;
        }
    }
}