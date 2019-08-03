using codeBank.Model;
using codeBank.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace codeBank.Data
    {
    public class bbSrv
        {
        bbAppContext db = new bbAppContext();

        public List<Kategori> lKategori(int uId)
            {
            
            return db.Kategories.Where(x => x.UstKategoriId == uId )
                             
                 //.OrderBy(x => x.Adi)
                 .ToList();
            }

        public List<Parca> listParca(int id)
            {
            return db.Parcas.Where(x => x.Kategori_Id == id)
                .OrderBy(x => x.Aciklama).ToList();
            }

        public int moveParca(int pid, int kid ) {
            Parca pd = db.Parcas.Where(x => x.Id == pid).FirstOrDefault();
            pd.Kategori_Id = kid;
            db.Entry(pd).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }

        public List<bbItems> listParca2(string q)
            {
            var r = (from l in db.Parcas
                         //where l.Kategori_Id == id
                     where l.Aciklama.ToLower().Contains(q.Trim().ToLower())
                     select new bbItems() { Text = l.Aciklama + " - " + l.Kategori.Adi, Value = l.Id }
                     ).OrderBy(x => x.Text).ToList();
            return r;
            }

        public List<bbItems> listParca2(int id)
            {
            var r = (from l in db.Parcas
                     where l.Kategori_Id == id
                     select new bbItems() { Text = l.Aciklama, Value = l.Id }
                     ).OrderBy(x => x.Text).ToList();
            return r;
            }

        public Parca GetParca(int id)
            {
            return db.Parcas.Where(x => x.Id == id).FirstOrDefault();
            }

        public int AddParca(Parca p)
            {
            db.Parcas.Add(p);
            p.KayitTarihi = DateTime.Now;
            p.KayitEden = Environment.UserName;
            return db.SaveChanges();
            }

        public int SaveParca(Parca p)
            {
            Parca pd = db.Parcas.Where(x => x.Id == p.Id).FirstOrDefault();
            pd.KayitTarihi = DateTime.Now;
            pd.KayitEden = Environment.UserName;

            db.Entry(pd).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
            }

        public Parca FindParca(int id)
            {
            return db.Parcas.Where(x => x.Id == id).FirstOrDefault();
            }

        public int DeleteParca(int id)
            {
            db.Parcas.Remove(FindParca(id));
            return db.SaveChanges();
            }

        public Kategori FindKategori(int id)
            {
            return db.Kategories.Where(x => x.Id == id).FirstOrDefault();
            }

        public int FindKategori(Kategori k)
            {
            return db.Kategories.Where(x => x.Adi == k.Adi && k.UstKategoriId == k.UstKategoriId).FirstOrDefault().Id;
            }

        public int DeleteKategori(int id)
            {
            db.Kategories.Remove(FindKategori(id));
            return db.SaveChanges();
            }

        public int RenameKategori(int id, string name)
            {
            db.Kategories.Where(x => x.Id == id).FirstOrDefault().Adi = name;
            return db.SaveChanges();
            }

        public int AddKategori(string adi, int ustId)
            {
            Kategori k = new Kategori();
            k.Adi = adi;
            k.UstKategoriId = ustId;

            k.KayitTarihi = DateTime.Now;
            k.KayitEden = Environment.UserName;


            db.Kategories.Add(k);
            if (db.SaveChanges() != 0)
                return FindKategori(k);
            return 0;
            }

        }
    }
