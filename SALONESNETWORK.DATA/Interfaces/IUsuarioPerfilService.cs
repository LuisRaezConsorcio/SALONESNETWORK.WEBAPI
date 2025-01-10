﻿using SALONESNETWORK.MODELS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALONESNETWORK.BLL.Interfaces
{
    public interface IUsuarioPerfilService
    {
        Task<bool> Insertar(UsuarioPerfil modelo);
        Task<bool> Actualizar(UsuarioPerfil modelo);
        Task<bool> Eliminar(int id);
        Task<UsuarioPerfil> ObtenerPorId(int id);
        Task<IQueryable<UsuarioPerfil>> ObtenerTodos();
    }
}
