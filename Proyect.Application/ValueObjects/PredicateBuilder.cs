﻿using System.Linq.Expressions;

namespace Project.Application.ValueObjects
{
	public static class PredicateBuilder
	{
		public static Expression<Func<T, bool>> True<T>() { return f => true; }
		public static Expression<Func<T, bool>> False<T>() { return f => false; }

		public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
															 Expression<Func<T, bool>> expr2)
		{
			var parameter = Expression.Parameter(typeof(T));

			var body = Expression.AndAlso(
				Expression.Invoke(expr1, parameter),
				Expression.Invoke(expr2, parameter)
			);

			return Expression.Lambda<Func<T, bool>>(body, parameter);
		}
	}

}
