﻿using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaRepository _repo;
    public CategoriasController(ICategoriaRepository repo)
    {
        _repo = repo;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoriaViewModel>> GetById(int id)
    {
        var categoria = await _repo.GetByIdAsync(id);
        if (categoria is null)
            return NotFound();

        return categoria;
    }

    [HttpPost]
    public async Task<ActionResult<CategoriaViewModel>> Create(CategoriaInputModel categoria)
    {
        var resp = await _repo.CreateAsync(categoria);
        if (resp is null) return BadRequest();
        return Ok(resp);
    }
}