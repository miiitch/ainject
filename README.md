# AInject

A proxy for "Application Insights" that supports dependency injection

# What is AInject?

AInject is a thin layer on top Application Insight. Its goal is to simplify the use of Application Insight for the following cases:
* telemetry injection
* metadata usage

# Nuget packages

AInject is a set of packages:
* Ainject.Abstractions: a set of interfaces and generic componants with no relationship with Application Insights. To use in libraries and tests
* Ainject.Ainject.ApplicationInsights: the library that links Application Insights to the AInject interfaces