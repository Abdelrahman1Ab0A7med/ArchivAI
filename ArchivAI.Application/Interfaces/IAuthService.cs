using ArchivAI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArchivAI.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDTO>RegisterAsync(RegisterDTO registerDTO);
        Task<AuthResponseDTO>LoginAsync(LoginDTO loginDTO);
    }
}
