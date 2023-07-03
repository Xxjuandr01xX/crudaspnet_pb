using System.ComponentModel.DataAnnotations;
namespace CRUD.Models
{
    public class ContactosModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo nombre es requerido !")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo apellido es requerido !")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "El campo cedula es requerido !")]
        public string Cedula { get; set; }
        [Required(ErrorMessage = "El campo telefono es requerido !")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "El campo correo es requerido !")]
        public string  Correo { get; set; }
        [Required(ErrorMessage = "El campo direccion es requerido !")]
        public string Direccion { get; set; }

    }
}
