using System.ComponentModel.DataAnnotations;

namespace codeBank.Model
    {
    public class Kategori :bbBase
    {
      

        [MaxLength(255)]
        public string Adi { get; set; }
        [MaxLength(255)]
        public string Aciklama { get; set; }

        public int UstKategoriId { get; set; }

    }
}
