// ----------------------------------------------
// How to enable exception fault reporting
// ----------------------------------------------

// METHOD 1 : Declarative (using config file)

<configuration>
  <system.serviceModel>
    <behaviors>
      <serviceBehaviors>
        <behavior name="debug">
          <serviceDebug includeExceptionDetailInFaults="true" />
        </behavior>
      </serviceBehaviors>
    </behaviors>
    ...
  </system.serviceModel>
</configuration>

Then apply the behavior to your service along these lines:

<configuration>
  <system.serviceModel>
    ...
    <services>
      <service name="MyServiceName" behaviorConfiguration="debug" />
    </services>
  </system.serviceModel>
</configuration>


// METHOD 2: Imperative (Programming)

ServiceDebugBehavior debug = Sh.Description.Behaviors.Find<ServiceDebugBehavior>();
            if (debug == null)
            {
                Sh.Description.Behaviors.Add(
                     new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });
            }
            else
            {
                // make sure setting is turned ON
                if (!debug.IncludeExceptionDetailInFaults)
                {
                    debug.IncludeExceptionDetailInFaults = true;
                }
            }