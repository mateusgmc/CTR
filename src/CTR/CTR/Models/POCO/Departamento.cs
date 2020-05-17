﻿using System.Collections.Generic;

namespace CTR.Models.POCO
{
    public class Departamento
    {
        public int DepartamentoId { get; set; }

        public string Descricao { get; set; }

        public int SecretariaId { get; set; }
        public virtual Secretaria Secretaria { get; set; }

        public virtual ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();
    }
}