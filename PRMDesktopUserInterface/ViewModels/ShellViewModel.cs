using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using PRMDesktopUserInterface.EventModels;

namespace PRMDesktopUserInterface.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>

    {
        private SalesViewModel _salesVM;
        private IEventAggregator _events;
        
        private SimpleContainer _container;
        public ShellViewModel(IEventAggregator events, SalesViewModel salesVM, SimpleContainer container)
        {
            _salesVM = salesVM;
            _events = events;
            _container = container;

            _events.Subscribe(this);
            ActivateItem(_container.GetInstance<LoginViewModel>());

            


        }

        public void Handle(LogOnEvent message)
        {
            ActivateItem(_salesVM);
            
        }
    }
    
}
