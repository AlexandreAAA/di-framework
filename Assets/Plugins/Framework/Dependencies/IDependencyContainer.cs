﻿using System;
using JetBrains.Annotations;

namespace Framework.Dependencies
{
	public interface IDependencyContainer
	{
		bool TryResolve([NotNull] Type type, out object dependency);
		void EnsureInitialized();

		bool CanBeResolvedSafe([NotNull] Type type);
	}
}