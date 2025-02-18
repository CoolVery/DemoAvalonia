using System;
using System.Collections.Generic;
using System.Linq;
using Demo.Models;
using Demo.Views;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;

namespace Demo.ViewModels
{
	public class ListPartnersViewModel : ReactiveObject
	{
		List<Partner> partners;
        List<Partner> partnersCopy;
        string namePartner = "";
		List<PartnerType> partnersType;
		PartnerType selectedPartnerType;
        public List<Partner> Partners { get => partners; set => this.RaiseAndSetIfChanged(ref partners, value); }
        public string NamePartner { 
			get => namePartner; 
			set 
			{ 
				this.RaiseAndSetIfChanged(ref namePartner, value);
                AllFilters(); 
			} 
		}

        public List<PartnerType> PartnersType { get => partnersType; set => this.RaiseAndSetIfChanged(ref partnersType, value); }
        public PartnerType SelectedPartnerType {
			get => selectedPartnerType;
			set
			{
				selectedPartnerType = value; 
				AllFilters();
			}
		}

        public ListPartnersViewModel()
		{
			partners = MainWindowViewModel.Inctance.Context.Partners.Include(x => x.PartnerType).ToList();
			partnersCopy = new List<Partner>(partners);
			partnersType = MainWindowViewModel.Inctance.Context.PartnerTypes.ToList();
			partnersType.Add(new PartnerType() { PartnerTypeName = "Нет фильтра"});
        }
		void AllFilters()
		{
			SearchPartnerName();
			SerchPartnerType();

        }
		void SearchPartnerName()
		{
			switch (string.IsNullOrEmpty(namePartner))
			{
				case true:
					Partners = partnersCopy;
					break;
                case false:
					Partners = partnersCopy.Where(partner => partner.PartnerName.Contains(namePartner)).ToList();
                    break;
            }
		} 
		void SerchPartnerType()
		{
			switch (selectedPartnerType == null || selectedPartnerType.PartnerTypeName == "Нет фильтра")
			{
				case true:
					break;
				case false:
					Partners = Partners.Where(partners => partners.PartnerType == SelectedPartnerType).ToList();
					break;
			}
		}
		public void ToAddNewPartner()
		{
			MainWindowViewModel.Inctance.CurrentUC = new NewPartner();
		}
	}
}