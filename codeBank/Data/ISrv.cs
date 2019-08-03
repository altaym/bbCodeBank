using codeBank.Model;
using codeBank.Utils;
using System.Collections.Generic;

namespace codeBank.Data
    {
    interface IbbSrv
        {
        List<Kategori> lKategori(int uId);
        List<Parca> listParca(int id);
        int moveParca(int pid, int kid);
        List<bbItems> listParca2(string q);
        List<bbItems> listParca2(int id);
        Parca GetParca(int id);
        int AddParca(Parca p);
        int SaveParca(Parca p);
        Parca FindParca(int id);
        int DeleteParca(int id);
        Kategori FindKategori(int id);
        int FindKategori(Kategori k);
        int DeleteKategori(int id);
        int RenameKategori(int id, string name);
        int AddKategori(string adi, int ustId);

        }
    }
