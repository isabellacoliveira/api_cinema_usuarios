using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FilmesApi.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using FluentResults;

namespace FilmesAPI.Services 
{
    public class FilmeService
    {
        private AppDbContext _context;
        private IMapper _mapper;
    public FilmeService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
        public ReadFilmeDto AdicionaFilme(CreateFilmeDto filmeDto)
        {
             Filme filme = _mapper.Map<Filme>(filmeDto);
            _context.Filmes.Add(filme);
            _context.SaveChanges();
            // vamos retornar um mapeamento de um filme criado para um filme dto
            return _mapper.Map<ReadFilmeDto>(filme); 
        }

// estamos gerando ele dentro do nosso serviço mas os métodos Ok e notFound,
// são respostas de requisições que estamos enviando, o papel disso é do controlador
// se a resposta dos filmes for diferente de nula vamos retornar o readDto 
        public List<ReadFilmeDto> RecuperarFilmes(int? classificacaoEtaria)
        {
               List<Filme> filmes;
            if (classificacaoEtaria == null)
            {
                 filmes = _context.Filmes.ToList();
            }
            else
            {
                filmes = _context
                .Filmes.Where(filme => filme.ClassificacaoEtaria <= classificacaoEtaria).ToList();
            }
            
            if(filmes != null)
            {
                List<ReadFilmeDto> readDto = _mapper.Map<List<ReadFilmeDto>>(filmes);
                return readDto;
            }
            // caso passemos pelo cenário acima e não retornemos nada, vamos retornar uma lista nula 
            return null;
        }

        public ReadFilmeDto RecuperarFilmesPorId(int id)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme != null)
            {
                ReadFilmeDto filmeDto = _mapper.Map<ReadFilmeDto>(filme);

                return filmeDto;
            }
            return null;
        }

        public Result AtualizaFilme(int id, UpdateFilmeDto filmeDto)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null)
            {
                return Result.Fail("Filme não encontrado");
            }
            _mapper.Map(filmeDto, filme);
            _context.SaveChanges();
            return Result.Ok();
        }

        public Result DeletaFilme(int id)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null)
            {
                return Result.Fail("Filme não encontrado");
            }
            _context.Remove(filme);
            _context.SaveChanges();
            return Result.Ok();
        }
    }
}