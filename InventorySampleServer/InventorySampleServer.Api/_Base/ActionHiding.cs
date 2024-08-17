using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace InventorySampleServer.Api._Base
{
	public class ActionHiding : IActionModelConvention
	{
		public void Apply(ActionModel action)
		{
			if (action.Controller.ControllerName.StartsWith("G"))
				action.ApiExplorer.IsVisible = false;
		}
	}
}
