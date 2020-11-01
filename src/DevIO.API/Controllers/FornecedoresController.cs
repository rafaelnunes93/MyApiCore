﻿using AutoMapper;
using DevIO.API.ViewModels;
using DevIO.Business.Intefaces;
using DevIO.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevIO.API.Controllers
{
    [Route("api/fornecedores")]
    public  class FornecedoresController : MainController
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IFornecedorService _fornecedorService;
        private readonly IMapper _mapper;

        public FornecedoresController(IFornecedorRepository fornecedorRepository,
                                        IMapper mapper, IFornecedorService fornecedorService)
        {
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
            _fornecedorService = fornecedorService;
        }

        [HttpGet]
        public async Task<IEnumerable<FornecedorViewModel>> ObterTodos()
        {
            var fornecedor = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());
            return fornecedor;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<FornecedorViewModel>>ObterPorId(Guid id)
        {
            var fornecedor = await ObterFornecedoresProdutosEndereco(id);

            if (fornecedor == null) return NotFound();

            return fornecedor;
        }

        [HttpPost]
        public async Task<ActionResult<FornecedorViewModel>> Adicionar(FornecedorViewModel fornecedorViewModel)
        {
            if (!ModelState.IsValid) return BadRequest();

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
            var result =  await _fornecedorService.Adicionar(fornecedor);

            if (!result) return BadRequest();

            return Ok(fornecedor);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<FornecedorViewModel>> Atualizar(Guid id, FornecedorViewModel fornecedorViewModel)
        {
            if(id != fornecedorViewModel.Id) 

            if (!ModelState.IsValid) return BadRequest();

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
            var result = await _fornecedorService.Atualizar(fornecedor);

            if (!result) return BadRequest();

            return Ok(fornecedor);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<FornecedorViewModel>> Excluir(Guid id)
        {
            var fornecedor = await ObterFornecedoresEndereco(id);

            if (fornecedor == null) return NotFound();

            var result = await _fornecedorService.Remover(id);

            if (!result) return BadRequest();

            return Ok(fornecedor);
        }



        public async Task<FornecedorViewModel> ObterFornecedoresProdutosEndereco(Guid id)
        {
           return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorProdutosEndereco(id));
        }

        public async Task<FornecedorViewModel> ObterFornecedoresEndereco(Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorEndereco(id));
        }



    }
}
    