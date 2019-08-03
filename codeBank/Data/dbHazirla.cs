using System.Collections.Generic;
using System.Data.Entity;
using codeBank.Model;

namespace codeBank.Data
    {
    public class dbHazirla : CreateDatabaseIfNotExists<bbAppContext>
    {
        protected override void Seed(bbAppContext context) {

            List<Kategori> kl = new List<Kategori> {
                new Kategori { Adi="C#",Aciklama="C#",UstKategoriId=0},
                new Kategori { Adi="For",Aciklama="For Döngüsü",UstKategoriId=1},
                new Kategori { Adi="Foreach" , Aciklama="Foreach Döngüsü",UstKategoriId=1},
                new Kategori { Adi="While", Aciklama="While Döngüsü",UstKategoriId=1}
            };

            kl.ForEach(c => context.Kategories.Add(c));

            context.SaveChanges();
            base.Seed(context);
        }

    }
}
