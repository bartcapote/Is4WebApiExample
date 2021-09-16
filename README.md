The application uses an EF Core in memory database provider, so no SQL configuration is needed.
Just build and run.

Please ignore hardcoded values and comments (for the most part at least).


Note:
Currently the place in which user location (path within domain) during authentication flow persists is session storage.

Probably a safer way would be to "inject" it into userManager config and register all possible redirect uris server-side.

Food for thought but woould recommend the latter approach in production.

The third way is to use ExtraQueryParams in said config, but it seems to add unnecessary complexity (though allows for dynamic server-side validation at runtime).
