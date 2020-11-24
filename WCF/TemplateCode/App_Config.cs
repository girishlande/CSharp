// -------------------
// TCP end points 
// -------------------
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


// ---------------------------------------------------
// Multiple end points TCP , HTTP , NET PIPE 
// ---------------------------------------------------
<system.serviceModel>
		<behaviors>
			<serviceBehaviors>
				<behavior name="MyBeh">
					<serviceMetadata/>
				</behavior>
			</serviceBehaviors>
		</behaviors>
		<services>
			<service name="AjitLibrary.Ajit" behaviorConfiguration="MyBeh">
				<endpoint address="net.tcp://localhost:9090/MyTcpEndPoint" binding="netTcpBinding"
                    bindingConfiguration="" contract="AjitLibrary.IAjit" />

				<endpoint address="net.tcp://localhost:9090/MyTcpEndPoint/mex" binding="mexTcpBinding"
                        bindingConfiguration="" contract="IMetadataExchange" />

				<endpoint address="http://localhost:9091/MyHttpEndPoint" binding="basicHttpBinding"
                    bindingConfiguration="" contract="AjitLibrary.IAjit" />

				<endpoint address="http://localhost:9091/MyHttpEndPoint/mex" binding="mexHttpBinding"
                        bindingConfiguration="" contract="IMetadataExchange" />

				<endpoint address="net.pipe://localhost/MyPipeEndPoint" binding="netNamedPipeBinding"
                    bindingConfiguration="" contract="AjitLibrary.IAjit" />

				<endpoint address="net.pipe://localhost/MyPipeEndPoint/mex" binding="mexNamedPipeBinding"
                        bindingConfiguration="" contract="IMetadataExchange" />


			</service>
		</services>
	</system.serviceModel>
	