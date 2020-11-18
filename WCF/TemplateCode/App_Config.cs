<?xml version="1.0" encoding="utf-8" ?>
<configuration>
    <startup> 
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.5" />
    </startup>
    <system.serviceModel>
      <behaviors>
        <serviceBehaviors>
          <behavior name="MyBeh">
            <serviceMetadata/>
          </behavior>
        </serviceBehaviors>
      </behaviors>
        <services>
            <service name="MulServiceLibrary.MulService" behaviorConfiguration="MyBeh">
                <endpoint address="net.tcp://localhost:9090/MyTcpEndPoint" binding="netTcpBinding"
                    bindingConfiguration="" contract="MulServiceLibrary.IMulService" />
            <endpoint address="net.tcp://localhost:9090/MyTcpEndPoint/mex" binding="mexTcpBinding"
                    bindingConfiguration="" contract="IMetadataExchange" />
            </service>
        </services>
    </system.serviceModel>
</configuration>