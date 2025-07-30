using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeApp.Domain.Entities;
public class Anime
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Diretor { get; set; } = string.Empty;
    public string Resumo { get; set; } = string.Empty;
}
