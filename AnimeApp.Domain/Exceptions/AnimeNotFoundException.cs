using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeApp.Domain.Exceptions;

public class AnimeNotFoundException(Guid id) : Exception($"Anime com ID {id} não encontrado.")
{
}

