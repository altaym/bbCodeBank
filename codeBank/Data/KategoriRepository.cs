using codeBank.Model;

namespace codeBank.Data
    {
    public class KategoriRepository : GenericRepository<Kategori>, IKategoriRepository
        {
        public int RenameKategori(int id, string name)
            {
            //_db.Kategories.Where(x => x.Id == id).FirstOrDefault().Adi = name;
            //return _db.SaveChanges();
            Kategori k = Get(id);
            k.Adi = name;

            return Update(k);
            }
        }
    }
