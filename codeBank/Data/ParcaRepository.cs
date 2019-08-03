using codeBank.Model;

namespace codeBank.Data
    {
    public class ParcaRepository :  GenericRepository<Parca>, IParcaRepository
        {
        public int moveParca(int pid, int kid)
            {
            //Parca pd = db.Parcas.Where(x => x.Id == pid).FirstOrDefault();
            //pd.Kategori_Id = kid;
            //db.Entry(pd).State = System.Data.Entity.EntityState.Modified;
            //return db.SaveChanges();
            Parca p = Get(pid);
            p.Kategori_Id = kid;
            return Update(p);
            }
        }
    }
