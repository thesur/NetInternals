using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HandledDll
{
    abstract internal class LowLevelHook
    {
        private uint PrologueBytesToCopy = 0;

        private IntPtr pModule = IntPtr.Zero;
        private IntPtr pFunction = IntPtr.Zero;

        internal Hook Hook { get; set; }

        public LowLevelHook(Hook hook, uint prologueLength)
        {
            this.Hook = hook;
            this.PrologueBytesToCopy = prologueLength;
            pModule = WinApi.WinApi.GetModuleHandle(hook.Module);
            pFunction = WinApi.WinApi.GetProcAddress(pModule, hook.Function);
        }

        /// <summary>
        /// Enables the hook
        /// </summary>
        /// <returns>Returns the memory address that is just after the inline hook. This is the address where the managed code has to jump after processing the hook</returns>
        protected IntPtr Enable(IntPtr pHandle, Delegate callBack)
        {
            ManagedDll.ManagedDll.InterComClient.Log("Enabling (pHandle: " + pHandle.ToString() + ")");

            IntPtr jmp = IntPtr.Zero;
            IntPtr bytes = IntPtr.Zero;

            jmp = ClonePrologue(pHandle, pFunction, PrologueBytesToCopy); // bytesToCopy are the number of bytes of the prologue of the function to copy

            // To do:
            // Calculate the opcodes of the prolog of the API, so we make sure that
            // the JMP is done as an instruction and isn't set in the middle of a opcode.

            byte[] jmpInApi = new byte[] {
                0xB8, 0x00, 0x00, 0x00, 0x00, // MOV EAX, addr
			    0xFF, 0xE0                    // JMP EAX
		    };
            try
            {
                Array.Copy(BitConverter.GetBytes((uint)Marshal.GetFunctionPointerForDelegate(callBack)), 0, jmpInApi, 1, 4);

                WinApi.WinApi.WriteProcessMemory(pHandle, pFunction, jmpInApi, jmpInApi.Length, out bytes);
            }
            catch (Exception ex)
            {
                ManagedDll.ManagedDll.InterComClient.Log(ex.Message);
            }

            return new IntPtr((uint)jmp);

        }

        /// <summary>
        /// Clones the prologue of the API into another memory position. 
        /// </summary>
        /// <param name="processHandle">process handle</param>
        /// <param name="pFunction">position where the original function is located</param>
        /// <param name="nBytes">number of bytes to copy</param>
        /// <returns>Returns the memory address where the cloned code is located</returns>
        private IntPtr ClonePrologue(IntPtr processHandle, IntPtr pFunction, uint nBytes)
        {
            byte[] asm = new byte[nBytes + 5];
            IntPtr bytesRead = IntPtr.Zero;
            WinApi.WinApi.ReadProcessMemory(processHandle, pFunction, asm, nBytes, out bytesRead);
            byte[] jmp = new byte[] { 0xe9, 0x00, 0x00, 0x00, 0x00 }; // JMP addr
            IntPtr pos = WinApi.WinApi.VirtualAllocEx(processHandle, IntPtr.Zero, 20, WinApi.WinApi.AllocationType.Commit | WinApi.WinApi.AllocationType.Reserve, WinApi.WinApi.MemoryProtection.ReadWrite);

            uint whereToJump = 0;
            //if ((int)pos < (int)pFunction)
            //    whereToJump = (uint)pFunction + nBytes - (uint)pos - 15 + 2;
            //else
            whereToJump = (uint)pFunction + nBytes - (uint)pos - 15;

            Array.Copy(BitConverter.GetBytes(whereToJump), 0, jmp, 1, 4);
            Array.Copy(jmp, 0, asm, nBytes, jmp.Length);

            WinApi.WinApi.WriteProcessMemory(processHandle, pos, asm, asm.Length, out bytesRead);
            return pos;
        }

        internal abstract void Enable(IntPtr pHandle);
    }
}
