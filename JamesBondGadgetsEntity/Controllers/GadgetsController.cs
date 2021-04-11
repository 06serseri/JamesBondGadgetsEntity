﻿
using JamesBondGadgetsEntity.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JamesBondGadgetsEntity.Controllers
{
    public class GadgetsController : Controller
    {

        private ApplicationDbContext context;

        public GadgetsController()
        {
            context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        // GET: Gadgets
        public ActionResult Index()
        {
            List<GadgetModel> gadgets = context.Gadgets.ToList();

            return View("Index", gadgets);
        }

        public ActionResult Details(int id)
        {
            GadgetModel gadget = context.Gadgets.SingleOrDefault(g => g.Id == id);

            return View("Details", gadget);
        }

        //Create new gadget in the database
        public ActionResult Create()
        {
            return View("GadgetForm", new GadgetModel());
        }

        public ActionResult Edit(int id)
        {
            GadgetModel gadget = context.Gadgets.SingleOrDefault(g => g.Id == id);

            return View("GadgetForm", gadget);
        }

        public ActionResult Delete(int id)
        {
            GadgetModel gadget = context.Gadgets.SingleOrDefault(g => g.Id == id);
            context.Entry(gadget).State = EntityState.Deleted;
            context.SaveChanges();

            return Redirect("/Gadgets");
        }

        public ActionResult ProcessCreate (GadgetModel gadgetModel)
        {
            //Save to the database
            GadgetModel gadget = context.Gadgets.SingleOrDefault(g => g.Id == gadgetModel.Id);

            //Edit
            if (gadget != null)
            {
                gadget.Name = gadgetModel.Name;
                gadget.Description = gadgetModel.Description;
                gadget.AppearsIn = gadgetModel.AppearsIn;
                gadget.WithThisActor = gadgetModel.WithThisActor;
            }
            else
            {
                context.Gadgets.Add(gadgetModel);
            }
            context.SaveChanges();

            return View("Details", gadgetModel);
        }

        public ActionResult SearchForm()
        {
            return View("SearchForm");
        }


        public ActionResult SearchForName(string searchPhrase)
        {
            var gadgets = from g in context.Gadgets where g.Name.Contains(searchPhrase) select g;

            return View("Index", gadgets);
        }

        public ActionResult SearchForDescription(string searchPhraseDescription)
        {
            var gadgets = from g in context.Gadgets where g.Description.Contains(searchPhraseDescription) select g;

            return View("Index", gadgets);
        }

    }
}