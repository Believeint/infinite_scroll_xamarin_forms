using InfiniteScrollingExample4.Models;
using InfiniteScrollingExample4.Service;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace InfiniteScrollingExample4.ViewModels
{
	public class OrderListViewModel : BaseViewModel
	{
		private OrderService _orderService = new OrderService();
		private PessoaService _pessoasService = new PessoaService();
		private ObservableRangeCollection<Order> _orders = new ObservableRangeCollection<Order>();
		private ObservableRangeCollection<Pessoa> _pessoas = new ObservableRangeCollection<Pessoa>();

		private ICommand _refreshCommand, _loadMoreCommand = null;
		private ICommand _loadMoreCommandPessoas, _refreshCommandPessoas = null;

		public OrderListViewModel()
		{
			//LoadOrders();
			LoadPessoas();
		}

		public async Task LoadOrders()
		{
			var newOrders = _orderService.Load(null);

			_orders.ReplaceRange(newOrders);

			Title = $"Orders {_orders.Count}";
		}

		public async Task LoadPessoas()
		{
			var novasPessoas = _pessoasService.Carregar(null);
			_pessoas.ReplaceRange(novasPessoas);
			Title = $"Pessoas {_pessoas.Count}";
		}

		public ObservableRangeCollection<Order> Orders
		{
			get { return _orders; }

		}

		public ObservableRangeCollection<Pessoa> Pessoas
		{
			get { return _pessoas; }
		}

		public ICommand RefreshCommand
		{
			get { return _refreshCommand ?? new Command(async () => await ExecuteRefreshCommand(), () => CanExecuteRefreshCommand()); }
		}

		public ICommand RefreshCommandPessoas
		{
			get { return _refreshCommandPessoas ?? new Command(async () => await ExecuteRefreshCommand(), () => CanExecuteRefreshCommand()); }
		}

		public bool CanExecuteRefreshCommand()
		{
			return IsNotBusy;
		}

		public async Task ExecuteRefreshCommand()
		{
			IsBusy = true;

			await LoadOrders();

			IsBusy = false;
		}

		public ICommand LoadMoreCommand
		{
			get { return _loadMoreCommand ?? new Command<Order>(ExecuteLoadMoreCommand, CanExecuteLoadMoreCommand); }
		}

		public ICommand LoadMoreCommandPessoas
		{
			get { return _loadMoreCommandPessoas ?? new Command<Pessoa>(ExecuteLoadMoreCommandPessoas, CanExecuteMoreCommandPessoas); }
		}

		public bool CanExecuteMoreCommandPessoas(Pessoa pessoa)
		{
			return IsNotBusy && _pessoas.Count != 0 && _pessoas.OrderByDescending(o => o.Id).Last().Id == pessoa.Id;
		}

		public bool CanExecuteLoadMoreCommand(Order item)
		{
			return IsNotBusy && _orders.Count != 0 && _orders.OrderByDescending(o => o.Created).Last().Created == item.Created;
		}

		public void ExecuteLoadMoreCommand(Order item)
		{
			IsBusy = true;
			var items = _orderService.Load(item.Created);
			_orders.AddRange(items);
			Title = $"Orders {_orders.Count}";
			IsBusy = false;
		}

		public void ExecuteLoadMoreCommandPessoas(Pessoa pessoa)
		{
			IsBusy = true;
			var items = _pessoasService.Carregar(pessoa.Id);
			_pessoas.AddRange(items);
			Title = $"Pessoas {_pessoas.Count}";
			IsBusy = false;
		}


	}
}
