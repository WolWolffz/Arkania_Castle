# Arkania_Castle

RESUMO INICIAL
=======================================
plataforma com tower defense e elemento de cartas.
O jogo funciona basicament escolhendo cartas de um baralho definido pelo jogador para que criaturas apareçam na base de uma torre (visão estilo jogo do mario original/donkey kong).
Essa torre tem varias escadas que definem diversos caminhos, todos visiveis, até o topo.

O jogador precisa escolher as cartas e jogá-las na base da torre. Depois, irá escolher por qual escada as tropas subirão. Dessa forma, elas começarão a subir a torre. O objetivo de cada fase é subir até o topo, com cada fazse tendo caminhos diferentes.

Contudo, criaturas irão descer as escadas pelos caminhos ou aparecerão pelas laterais de certos quartos. Quando uma tropa encontra uma criatura no mesmo andar, elas se batem até uma delas morrer. Se a criatura ganha, ela desce um nivel. Se a tropa ganha, ela sobe um nível. Se as criaturas chegam até a base, a fase é perdida.

Diferentes inimigos e diferentes tropas podem ter propriedades, vida e danos diferentes. Podem haver cartas coringas como zap, bombas, power ups, etc.


FORMATO DO JOGO
======================================
--cartas e baralho

O jogador poderá selecionar diversas cartas com diversas propriedades diferentes para jogar no baralho

O baralho tem um tamanho pré definido (a definir) e pode ser construído com cartas repetidas (quantas? ou não?)

Os tipos de cartas contam com propriedades diferentes

-aliados

os aliados são as cartas padrão de criaturas, que poderão ser desbloqueadas ao decorrer do jogo (compra,desbloqueio ao longo dos niveis, etc)

-magias

as magias são cartas com efeito imediato que causam algo, seja num aliado, num inimigo ou no jogador como em seu baralho

-lendas

são como os aliados, mas são criaturas desbloqueadas no final ou deccorer de cada estágio (bosses, resgatados, etc)

-construcots

constructos são objetos que podem ser instalados numa sala onde já haja ao menos um aliado. cada sala pode conter um constructo, e cada constructo pode ter algum tipo de efeito

--mapa

o mapa do jogo pode ser dividido em dois: em jogo e em navegação.

Na navegação o jogador escolhe em qual estágio jogar (como na seleção de niveis do candy crush ou qualquer outro jogo mobile)

O mapa em jogo é onde a ação acontece. Ele é dividido em:

-niveis

um nivel seria um andar. em cada andar podem haver mais de uma sala ou caminhos.

-salas

a sala é onde as criaturas ficam e os efeitos magicos são depositados. as salas podem ser ou não conectadas por caminhos

-caminhos 

caminhos interligam diferentes salas em diferentes andars. caminhos podem percorrer mais de um nível para inteligar duas salas. Caminhos nunca são na horizontal

--inimigos

os inimigos (goblins ou outros monstros fantásticos) começam no topo do nível e em alguns níveis pré-selecionados. esse inimigos podem ou não descer através dos caminhos. cada inimigo deve ter uma certa quantidade de vida, dano e, talvez, uma propriedade/habilidade especial.

Durante a partida, os inimigos descem os níveis e, se chegarem ao solo, o jogador perde essa partida

--bosses

bosses são inimigos mais fortes, normalmente encontrados no final de estágios. esses bosses se tranformam em cartas (lendas) depois de derrotados, normalmente com boas habilidades mas custo alto (custo, limitar quantidade no baralho, etc)

--visão do jogador

na tela do jogo, ele verá:

-mapa/fase

a fase consiste em um conjunto de salas em diferentes níveis, interligadas por caminhos. o objetivo é chegar ao topo. o jogador consegue ver o nível/torre por inteiro (arrastar tela, espremer para caber, etc)

-mão

a mão do jogador é fixa na parte inferior da tela. a mão mostra as cartas que o jogador tem disponível para jogar. além disso, mostra a economia da partida (moedas, energias, cargas, etc)

--jogabilidade

no começo da partida, o jogador verá o nível e sacará uma determinada quantidade de cartas do baralho. a jogabilidade é separada em etapas:

-Turno do Jogador

na manutenção, o jogador poderá jogar qualquer quantidade cartas e mover qualquer quantidade de aliados que ainda não se moveram (em qualquer ordem)

para mover os aliados, o jogador deve selecionar uma sala de origem e, em seguida, uma sala de destino que tenhaum caminho disponível. dessa forma, todas as tropas na sala de origem irão passar pelo caminho até a sala de destino. Quando se movimentam, também haverão prioridades com relação a quais tropas passam para a sala e em qual quantidade, a depender de seu tamanho e espaço disónível na sala de destino.

-Turno de Combate

Após finalizar o turno do jogador, todas as criaturas que estiverem nas mesmas salas (ou não, a depender de efeitos e habilidades) irão se combater. a ordem de ação no combate é: atacar constructos > atacar tropas por vida (mais vida para menos vida) > atacar tropas por quantidade de PV

por trás dos panos:

1-o jogo olhará a lista de criaturas dentro das salas que haverão combate

2-dado um combate, o jogo olhará qual lado tem mais criaturas (tropas ou inimigos)

3-o lado que tiver mais criaturas, o jogo ordenará por ataque em ordem decrescente

4-se não houver maior quantidade, tropas tem prioridade

5-o outro lado será ordenado em uma lista por vida em ordem decrescente

6-antes de atacar, o jogo olhará para as listas em busca de condições especiais (tanto para as listas do combate quanto para as outras listas em busca de condições externas)

7-tendo essas listas, o primeiro da lista ordenada por ataque atacará o primeiro da lista ordenada por vida

8-o atacante será removido da lista de combate e, se morrer, da lista da sala (kill)

9-o defensor será removido das listas de combate e da sala sala apenas se tiver morrido (de qualquer uma das listas APENAS se tiver morrido)

10-a lista de defensores será reordenada pela ordem decrescente de vida novamente

11-agora, quem está no topo da lista de atacante atacará o primeiro da lista de defensores

12-isso se repete até toda a lista de atacantes se esgotar ou toda a lista de defensores morrer

13-repetir a partir do passo 1 para todas as salas

-Turno dos inimigos
na vez dos inimigos, novos inimigos surgirão das origens e descerão um nível por um caminho, atacando os aliados q estiverem lá (ou não? tomarão dano tbm?).

se for uma fase de boss, esse boss pode tomar uma ação também.

Depois do turno dos inimigos, voltará para o turno do jogador

--economia/limitações

o jogo pode possuir uma economia no meio da partida, onde o jogador deve gerenciar os recursos para implementar a melhor estratégia. essa economia pode consistir de:

-dinheiros/manas etc que são coletados nas salas de alguma forma para serem utilizados para invocar cartas/determinadas cartas.

-dinheiros/manas que são utilizados fora da partida para comprar cartas de tropas.

-limite de cartas na mão, limite de cartas no baralho, limite de cartas repetidas no baralho.

modelos/exemplos
========================
cartas aliadas:

Soldado - PV10, ATK5
Homem com armadura completa de ferro e espada

Campeão - PV20, ATK7
Homem grande com roupa de guerra estilo romana, peitoral de metal e couro e capacete de metal. Com um machado grande nas mãos (aparenta ser maior que os outros)
-Tropa pesada

Escudeiro - PV20, ATK0, Habilidade: todo os inimigos atacarão o escudeiro primeiro.
Homem grande com escudo de torre de ferro

Curandeira - PV10, ATK4, Habilidade: todo turno, cura 1pv de todos os aliados na sala.
Mulher loira coberta com uma capa azul, com um orbe verde na mão.

Arqueiro - PV10, ATK4, Habilidade: na fase terminal, se não houver nenhum inimigo na mesma sala, ataca o inimigo com menos vida no nível superior.
Mulher jovem e baixa, Estilo Robin Hood, roupas leves com um chapéu de palha. Um arco simples nas mãos. 


cartas mágicas:

Recuperação - CUSTO10c, Recupere 10PV de todas as tropas numa sala.

Recuperação Total - CUSTO 20c, Recupere 10PV de todas as tropas num andar.

Raio - CUSTO 5c, Desfira 5 de dano em todos os inimigos numa sala.

Enfraquecimento - CUSTO 10c, Todos os inimigos numa sala desferem -2 de dano.

Cartas constructos:

Totem da recuperação total - PV20, CUSTO 10c, Habilidade: toda fase terminal, recupera 1pv vida de todos os aliados no nível.

Totem do baralho - PV10, CUSTO 20c, Habilidade: todo inicio de fase dos inimigos, compre uma carta.

Totem da proteção - PV10, CUSTO 20C, Habilidade: se alguma tropa morrer, ela volta com a vida total na fase terminal. o totem é destruído. 

Inimigos.

goblin - PV7, ATK4.

Gnoll - PV14, ATK5
Estética padrao de Gnoll. Espécie de hiena bipede. Adornos de osso, uma saia de couro. Usa uma cimitarra como arma

goblin arqueiro - PV7, ATK3, Habilidade: na fase terminal, se não houver nenhuma tropao na mesma sala, ataca a tropa com menos vida no nível de inferior.

orc - PV20, ATK 10.

xamã - PV 10, ATK 3, Habilidade: ao entrar numa sala, destrói qualquer constructo nela no lugar do ataque.
Estilo mini mago, baixinho com dois livros em sua orbita. Estilo Groot (de madeira)

evocador - PV 10, ATK3, Habilidade: +1pv por turno aos inimigos
Estilo camaleão feiticeiro com adornos, alto e com roupas góticas

Bosses.

Orc brandidor do machado - PV 50, ATK 8: Ataca 2 vezes, priorizando a tropa mais forte. Habilidade quando carta: ao invés de desferir 2 ataque de 8 de dano, desfere um ataque de 5 de dano em todos os inimigos numa sala.

Cão-plosão - PV 75, ATK 5, Habilidade: ataca 3 vezes, priorizando a tropa mais fraca. Habilidade quando carta: Quando morre, todas as criaturas na sala sofrem 5 de dano.


