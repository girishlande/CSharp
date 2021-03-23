var bindingCollection = binding.CreateBindingElements();
            NamedPipeTransportBindingElement bElement = new NamedPipeTransportBindingElement() { 
                MaxReceivedMessageSize = 20000000,
                MaxBufferSize = 20000000,
                MaxBufferPoolSize = 20000000
            };

            bindingCollection.Add(bElement);

            MwAcquisitionServiceHost = StartCtWcfService(address, baseAddressStr, typeof(AcquisitionService), typeof(IAcquisitionService), binding);
            if (MwAcquisitionServiceHost != null)
            {
                msg = "Acquisition Service is ready.";
                MwLogger.LogDebug(debugName, $"OnStart -- {msg}");
                Console.WriteLine(msg);
            }