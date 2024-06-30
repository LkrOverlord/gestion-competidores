using System;
using System.Collections.Generic;

namespace GestionDeCompetidores.Entidades.EF;

public partial class Deporte
{
    public int IdDeporte { get; set; }

    public string? NombreDeporte { get; set; }

    public virtual ICollection<Competidor> Competidors { get; set; } = new List<Competidor>();
}
