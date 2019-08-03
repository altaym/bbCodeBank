using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace codeBank.Model
    {
    public class Parca :bbBase
    {
      

        public int Kategori_Id { get; set; }

        [MaxLength(255)]
        public string Aciklama { get; set; }

        public string Veri { get; set; }

        [ForeignKey("Kategori_Id")]
        public Kategori Kategori { get; set; }

    }
}
