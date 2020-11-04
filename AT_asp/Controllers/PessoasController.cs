using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using Dominio.ViewModels;
using Dominio.Models;
using Dominio.Iterfaces;

namespace AT_asp.Controllers
{
    public class PessoasController : Controller
    {
        private readonly IAcesso _repository;

        public PessoasController(IAcesso repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> Listar()
        {
            List<ViewPessoas> vp = modelParaModelView(await _repository.ListaAssincrona());
            return View("List", vp);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Sobrenome,Aniversario")] ViewPessoas pessoas)
        {
            if (ModelState.IsValid)
            {
                var p = viewPessoaParaModel(pessoas);
                await _repository.Adicionar(p);
                await _repository.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(pessoas);
        }

        // GET: PessoasController
        public async Task<ActionResult> Index()
        {
            List<IList<ViewPessoas>> listas = new List<IList<ViewPessoas>>();
            var p = _repository.Ordenar();
            var pessoas = await _repository.ListaAssincrona();
            var aniversariantes = new List<Pessoas>();
            foreach (var item in pessoas)
            {
                if (item.Aniversario.Day == DateTime.Now.Day
                    && item.Aniversario.Month == DateTime.Now.Month)
                {
                    aniversariantes.Add(item);
                }
            }
            listas.Add(modelParaModelView(aniversariantes));
            listas.Add(modelParaModelView(p));
            
            return View(listas);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var pessoaEncontrada = _repository.Encontrar(id).Result;
            var vp = modelParaModelView(pessoaEncontrada);

            return View(vp);
        }
        
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)  
            {
                return NotFound();  
            }
            var P = await _repository.Encontrar(id);
            if(P == null)
            {
                return NotFound();
            }
            return View(modelParaModelView(P));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
        {
            Pessoas p = _repository.Encontrar(id).Result;
            if (p != null)
            {
                p.Nome = collection["Nome"];
                p.Sobrenome = collection["Sobrenome"];
                p.Aniversario = DateTime.Parse(collection["Aniversario"]);
                await _repository.SaveChanges();
                return View("Details", modelParaModelView(p));
            }
            return View("List");
        }

                
        public async Task<ActionResult> Delete(int id)
        {
            var pessoa = _repository.Encontrar(id).Result;

            _repository.remove(pessoa);
            await _repository.SaveChanges(); 

            return RedirectToAction(nameof(Index));
        }

        public IActionResult buscar(string nome)
        {
            List<Pessoas> encontrada = new List<Pessoas>();
            var v = _repository.ListaAssincrona().Result;
            foreach (Pessoas p in v)
            {
                if (p.Nome != null && !String.IsNullOrEmpty(nome)) 
                {
                    if (p.Nome.Contains(nome))
                    { 
                        encontrada.Add(p);
                    } 
                }
            }
            return View("List", modelParaModelView(encontrada)); 
        }

        private Pessoas viewPessoaParaModel(ViewPessoas vp)
        {
            var p = new Pessoas();
            p.Nome = vp.Nome;
            p.Sobrenome = vp.Sobrenome;
            p.Aniversario = vp.Aniversario;

            return p;
        }

        private ViewPessoas modelParaModelView(Pessoas pessoa)
        {
            var vp = new ViewPessoas();
            vp.id = pessoa.id;
            vp.Nome = pessoa.Nome;
            vp.Sobrenome = pessoa.Sobrenome;
            vp.Aniversario = pessoa.Aniversario;
            return vp;
        }

        private List<ViewPessoas> modelParaModelView(List<Pessoas> pessoas)
        {
            List<ViewPessoas> viewPessoas = new List<ViewPessoas>();
            foreach (Pessoas p in pessoas)
            {
                var vp = new ViewPessoas();
                vp.id = p.id;
                vp.Nome = p.Nome;
                vp.Sobrenome = p.Sobrenome;
                vp.Aniversario = p.Aniversario;
                viewPessoas.Add(vp);
            }
            return viewPessoas;
        }


    }
}

      