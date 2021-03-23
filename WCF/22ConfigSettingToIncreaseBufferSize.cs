<system.serviceModel>
		<bindings>
			<netNamedPipeBinding>
				<binding maxReceivedMessageSize="20000000"
 maxBufferSize="20000000"
 maxBufferPoolSize="20000000">
					<readerQuotas maxDepth="32"
		maxArrayLength="200000000"
		maxStringContentLength="200000000"/>
				</binding>
			</netNamedPipeBinding>
		</bindings>
	</system.serviceModel>
	
	<system.diagnostics>
		<sources>
			<source name="System.ServiceModel"
					switchValue="Error, Critical, Warning"
					propagateActivity="true" >
				<listeners>
					<add name="xml"/>
				</listeners>
			</source>
			<source name="myUserTracSource"
					switchValue="Error">
				<listeners>
					<add name="xml"/>
				</listeners>
			</source>
		</sources>
		<sharedListeners>
			<add name="xml"
				 type="System.Diagnostics.XmlWriterTraceListener"
					   initializeData="D:\logs\Traces.svclog" />
		</sharedListeners>
	</system.diagnostics>
	
	
	OR while hosting Do something like this.
	
	System.ServiceModel.Channels.Binding binding = new NetNamedPipeBinding() { MaxReceivedMessageSize = 20000};
	
	