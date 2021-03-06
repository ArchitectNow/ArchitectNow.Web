﻿using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ArchitectNow.Web.Middleware
{
	public class AngularRedirectMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly AngularRedirectMiddlewareOptions _options;

		public AngularRedirectMiddleware(RequestDelegate next, AngularRedirectMiddlewareOptions options = null)
		{
			_next = next;
			_options = options ?? new AngularRedirectMiddlewareOptions();
		}

		public async Task Invoke(HttpContext context)
		{
			await _next(context);

			var checkForPath = _options.CheckForPath;

			// If there's no available file and the request doesn't contain an extension, we're probably trying to access a page.
			// Rewrite request to use app root
			if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value) || checkForPath(context))
			{
				context.Request.Path = "/index.html"; // Put your Angular root page here 
				context.Response.StatusCode = 200; // Make sure we update the status code, otherwise it returns 404
				await _next(context);
			}
		}
	}
}