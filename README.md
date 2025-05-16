# OpenIddict Utils
Contains packages to assist in integrating Rock Solid Knowledge components into OpenIddict.

## Support
Here is a table detailing which versions of AdminUI support each version of the Utils package.

| Utils Package Version |    AdminUI Version    | Notes                                                                                      |
|:---------------------:|:---------------------:|--------------------------------------------------------------------------------------------|
|        v1.0.0         | v1.0-v1.1, v2.0, v3.0 | These versions of the Utils and AdminUI package are incompatible with the Rsk.Saml package |
|        v2.0.0         |  v1.2+, v2.1+, v3.1+  | Rsk.Saml package support was fixed from this point                                         |


## Scopes and Client Applications Claims
### Use case
In the OIDC protocol, it is common to assign Claims to both Scopes and Client Applications. Once this binding is in place, the associated Claims should be automatically included in the token when the access request is successful.

### Problem
OpenIddict does not provide a built-in mechanism for binding and assigning these Claims to Scopes or Client Applications by default.

### Solution
In AdminUI, Claims are stored within the Properties bag for each Scope or Client Application under the following keys:

- `urn:com:rocksolidknowledge:scope:property:claims` in the Scopes
- `urn:com:rocksolidknowledge:application:property:claims` in the Applications

To easily retrieve the assigned claims, you can use the extension method `GetClaimsFromProperties()` from both `IOpenIddictScopeManager` and `IOpenIddictApplicationManager`.

Aditionally you can call all the Claims grouped by Claim Types from `IOpenIddictApplicationManager` using `GetClaimValuesDictionary()`.