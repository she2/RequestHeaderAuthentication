# RequestHeaderAuthentication
An authentication scheme that helps validate/authenticate given http request header value in a clean way.

#### Usage
Usage is as simple as 
- Add to your `ConfigureServices` method in startup class 


```
 public IServiceProvider ConfigureServices(IServiceCollection services)
 {
           services.AddAuthentication()
                      .AddRequestHeaderAuthentication(RequestHeaderDefault.AUTHENTICATION_SCHEME_TWO, op =>
                        {
                            //specify the class to call for validating the request headers
                            op.Events = new SomeAuthEvents();

                            // The claim issuer
                            op.ClaimsIssuer = AuthenticationKeys.CLAIMS_ISSUER;
                            
                            // The header keys to validate
                            op.HeaderKey = new[] {
                                "X-ApiKey",
                                "X-Another-ApiKkey"
                            };
                        });
 }

 
```

- Create an event class that inherit from `RequestHeaderAuthenticationEvents` and override `ValidateToken` method

```
public class SomeAuthEvents : RequestHeaderAuthenticationEvents
    {

        public override async Task ValidateToken(ValidateTransXTokenContext context)
        {       
            // validate each request header here
        }
    }

```
- Add `app.UseHeaderAuth(TransXAuthDefault.AUTHENTICATION_SCHEME_ONE);` to the `Configure` method of the Startup class.

Please note that `TransXAuthDefault.AUTHENTICATION_SCHEME_ONE` can be replaced with any string but it must be same as what was specified in `ConfigureServices`.

### Header Validation

To validate a token, access the index from the collection of tokens via the `ValidateTransXTokenContext.TokenDetails` property.

e.g `TokenDetails tokenDetail = context.TokenDetails[i];` where `i` is the index of the token to be accessed.

- *A valid header*

If an header is valid, 

1. Set the `TokenValid` property of the selected token to `true`. For example,  `tokenDetail.TokenValid = false;` 
2. Create a set of claims, if required, to be added to the `ValidateTransXTokenContext`'s `ClaimsToSet` property . For example, `context.ClaimsToSet.AddRange(userClaims);`

- *An invalid header*

if an header is not valid,

1. Set the `TokenValid` property of the selected token to `false`. For example,  `tokenDetail.TokenValid = false;` 
2. Optionally set an error message on the `ValidateTransXTokenContext`'s `ErrorMessage` property . For example, `context.ErrorMessage = "some error message";`


If any of the tokens have the `TokenValid` property set to false, the validation will fail with a 403 error containing the error messages specified.