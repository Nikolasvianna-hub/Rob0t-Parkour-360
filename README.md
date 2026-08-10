# Rob0t Parkour 360 - Desafio Técnico Vortex (UNIFOR)

> 📦 **Download do Código-Fonte Completo (.zip):** 
> [Clique aqui para baixar o projeto completo no Google Drive](https://drive.google.com/file/d/1jMpj1oyv04bShvG5W2Eky2hqH9v4-WsP/view?usp=sharing)

# Navegador 360° Interativo - Desafio Técnico Vortex (UNIFOR)

Protótipo funcional de um navegador panorâmico 360° interativo desenvolvido na Unity para o processo seletivo de estágio do **Laboratório Vortex (UNIFOR)**.

---

## 🚀 Sobre o Projeto
O projeto simula a navegação panorâmica estilo Street View por locações reais (Fase 1: Unifor, Fase 2: Catedral, Fase 3: Dragão do Mar). O jogador navega no ambiente 360°, realiza mini-desafios de movimentação/parkour para atingir o ponto de vista ideal e interage com a tecla **F** para registrar a foto e visualizar as informações da locação.

### 🎮 Links e Acesso
* **Link da Build (WebGL):** [Link do jogo](https://nikolasvianna-hub.itch.io/rob0t-parkour-360)
* **Vídeo de Apresentação:** [Insira o link do YouTube/Vimeo aqui]

---

## 🛠️ Requisitos Técnicos Implementados
- [x] Desenvolvido na engine **Unity**
- [x] Estruturação modular em múltiplas cenas (`MainMenu`, `Fase 1 Unifor`, `Fase 2 Catedral`, `Fase 3 Dragão`)
- [x] Navegação e controle de câmera/movimentação via teclado e mouse
- [x] Sistema de interação e captura com feedback visual (Flash) e sonoro
- [x] Sistema de Pause funcional e reutilizável com fluxo de menus
- [x] Build exportada para **WebGL**
- [x] **Gamificação e Diferenciais (Bônus):** Mecânica de exploração espacial/parkour para atingir pontos de interesse, sistema de fotos e feedbacks visuais/sonoros integrados.

---

## 🤖 Diário de Bordo do uso de Inteligência Artificial

Em conformidade com a Seção 5 do desafio técnico, este trecho documenta o uso consciente de ferramentas de IA durante o desenvolvimento.

### 1. Ferramentas Utilizadas
- **Gemini / Claude:** Utilizados como copilotos de programação C# para Unity, depuração de erros de interface (UI Canvas) e estruturação do fluxo de arquitetura.

### 2. Prompts Importantes e Contextos
- *Ajuste de persistência e fluxo de cena:* "Como garantir que o Time.timeScale = 1f seja resetado ao trocar de cena do menu de pausa para o MainMenu sem travar a rotina?"
- *Encapsulamento do controller de foto:* "Como criar um script PhotoResultController que controle som, animação de flash e exibição do resultPanel de forma modular e expansível para Prefabs?"
- *Solução de UI/Canvas:* "Diagnóstico de erro em que o Panel ativava na Hierarchy, mas não renderizava na Game View."

### 3. Dificuldades Encontradas & Como a IA Ajudou
- **Hierarquia de UI e Canvas:** O painel de opções e pausa estava fora do Canvas raiz, o que fazia os elementos serem ativados na Hierarchy sem aparecerem na tela. A IA ajudou a diagnosticar que elementos de UI dependem obrigatoriamente do Canvas pai para renderização.
- **Conflitos de Referências cruzadas e Prefabs:** Ao reutilizar scripts de controle em diferentes cenas, algumas chamadas de eventos (`OnValueChanged` dos sliders) corromperam referências internas. A solução foi padronizar chamadas dinâmicas e isolar os componentes em Prefabs independentes.

### 4. Validação das Respostas da IA
Nenhuma linha de código ou instrução fornecida pela IA foi inserida no projeto sem validação manual no Unity Editor. A validação ocorreu via:
- Testes passo a passo no inspetor com o jogo rodando em modo Play.
- Correção de chamadas *Static Parameters* para *Dynamic Float* no sistema de eventos do Unity.
- Análise visual do `Rect Transform` e ancoragem para garantir que a UI se mantivesse responsiva.

### 5. Reflexão Crítica sobre o Uso de IA
A IA atuou como uma **ferramenta de produtividade e diagnóstico técnico (Thought Partner)**. Ela acelerou a resolução de travamentos e erros sintáticos, mas exigiu curadoria humana constante. Sem o entendimento prévio da arquitetura do Unity (como funcionamento de Cenas, Canvas, Event Systems e Prefabs), as sugestões de código teriam gerado inconsistências entre as cenas. A IA sugeriu caminhos, mas a decisões de design, física e escopo partiram inteiramente do desenvolvedor.

> 💡 **Nota sobre o Desenvolvimento:** Esta foi minha primeira experiência prática utilizando a Unity. Apesar de possuir bagagem prévia com Blender e Unreal Engine, o processo de adaptação às rotinas e à arquitetura da engine foi um desafio extremamente instigante e enriquecedor.
