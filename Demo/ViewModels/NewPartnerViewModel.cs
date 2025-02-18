using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Input;
using Demo.Models;
using ReactiveUI;

namespace Demo.ViewModels
{
	public class NewPartnerViewModel : ReactiveObject
	{
		Partner newPartner;
		
        List<PartnerType> partnersType;
        PartnerType selectedPartnerType;
        public Partner NewPartner { get => newPartner; set => this.RaiseAndSetIfChanged(ref newPartner, value); }
        public List<PartnerType> PartnersType { get => partnersType; set => this.RaiseAndSetIfChanged(ref partnersType, value); }
        public PartnerType SelectedPartnerType 
		{ 
			get => selectedPartnerType;
			set 
			{ 
				selectedPartnerType = value;
				NewPartner.PartnerType = selectedPartnerType;
			} 
		}

        

        public NewPartnerViewModel()
		{
			partnersType = MainWindowViewModel.Inctance.Context.PartnerTypes.ToList();
			
            newPartner = new Partner()
			{
				PartnerRating = 1,
				PartnerAdress = "",
				PartnerEmail = "",
				PartnerName = "",
				PartnerPhone = "",
				PartnerType = null,
				PartnerInn = "",
				PartnerDirectorCredentials = "",
				PartnerProducts = null,
				
			};
		}
		public void AddNewUser()
		{
			NewPartner.PartnerId = MainWindowViewModel.Inctance.Context.Partners.ToList().Count() + 1;
			MainWindowViewModel.Inctance.Context.Partners.Add(NewPartner);
			MainWindowViewModel.Inctance.Context.SaveChanges();
		}
    }
}