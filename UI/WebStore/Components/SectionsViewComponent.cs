﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces.Services;
using WebStore.Domain.ViewModels;

namespace WebStore.Components
{
    //[ViewComponent(Name ="CatalogSections")]
    public class SectionsViewComponent : ViewComponent
    {

        private readonly IProductData _ProductData;
        public SectionsViewComponent(IProductData ProductData) => _ProductData = ProductData;

        public IViewComponentResult Invoke(string SectionId)
        {
            var section_id = int.TryParse(SectionId, out var id) ? id : (int?)null;

            var sections = GetSections(section_id, out var parent_section_id);

            return View(new SectionCompleteViewModel
            {
                Sections = sections,
                CurrentSectionId = section_id,
                CurrentParrentSectionId = parent_section_id
            });
        }

        //public IViewComponentResult Invoke() => View(GetSections());

        //public async Task<IViewComponentResult> InvikeAsinc() { };

        //private IEnumerable<SectionViewModel> GetSections()
        private IEnumerable<SectionViewModel> GetSections(int? SectionId, out int? ParentSectionId)
        {
            ParentSectionId = null;
            var sections = _ProductData.GetSections().ToArray();

            var parent_sections = sections.Where(s => s.ParentId is null);

            var parent_section_views = parent_sections
                .Select(s => new SectionViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Order = s.Order
                })
                .ToList();

            foreach (var parent_section in parent_section_views) {

                var child = sections.Where(s => s.ParentId == parent_section.Id);

                foreach (var child_section in child)
                {
                    if (child_section.Id == SectionId)
                        ParentSectionId = parent_section.Id;

                    parent_section.ChildSections.Add(
                        new SectionViewModel
                        {
                            Id = child_section.Id,
                            Name = child_section.Name,
                            Order = child_section.Order,
                            ParentSection = parent_section
                        });
                }

                parent_section.ChildSections.Sort((a,b) => Comparer<int>.Default.Compare(a.Order, b.Order));
            }
            parent_section_views.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));
            return parent_section_views;
        }

    }
}
