﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DataAccess
{
    public class CursosRepositorio
    {
        private readonly Func<ApplicationContext> _contextBuilderFunction;

        public CursosRepositorio(Func<ApplicationContext> contextBuilderFunc)
        {
            _contextBuilderFunction = contextBuilderFunc;
        }

        /// Traer las materias con menos de x horas semanales con el plan z ordenados 
        /// en forma descendente por HsSemanales, incluyendo los datos del plan y la 
        /// especialidad asociados a esta
        public IEnumerable<Materia> GetMaterias(int hsSemanales, int anioPlan)
        {
            using (var context = _contextBuilderFunction())
            {
                return context.Materias
                    .Include(m => m.Plan).ThenInclude(p => p.Especialidad)
                    .Where(m => m.HsSemanales <= hsSemanales && m.Plan.Anio == anioPlan)
                    .OrderByDescending(m => m.HsSemanales).ToList();
            }
        }
    }
}
