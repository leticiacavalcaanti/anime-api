using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeApp.Application.DTOs;

public class AnimeDTO
{
    public Guid Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string Diretor { get; set; } = string.Empty;

    public string Resumo { get; set; } = string.Empty;
}