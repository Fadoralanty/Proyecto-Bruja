VAR Combate=false
VAR Moralidad = 0


Michael: Buenas tardes forastero, ¿En qué puedo ayudarle?
Richard: Si, soy un investigador privado y me han contratado para investigar la desaparición de 4 individuos que parece se están hospedando por aquí ¿Sabe usted algo?
Michael: No veo porqué debería darle información a nadie de mis inquilinos, menos si se sabe que están desaparecidos. 
Richard: ¿Es usted el dueño del hospedaje de esas personas?
Michael: Sí, ¿Y eso que le importa a usted? Ahora lárguese, de aquí antes que llame a la policía.
* [Intentar convencerlo]
~ Moralidad = 25
Richard: Pero señor, ¿Acaso no teme por la seguridad de sus inquilinos? Alguien podría demandarlo a usted por actividades sospechosamente ilegales en sus propiedades y hasta podría ir preso por obstrucción de una investigación.
Michael: Pfff, ¿Y quien me demandaría? Nadie puede demostrar que yo tenga nada que ver, además, la obstrucción tendría sentido si trabajases con la policía y tú mismo dijiste que eres un investigador privado.
** [Mencionar al Sheriff Williams]
~ Moralidad = 25
Richard:  Señor, si sigue hablando así realmente deberé llamar al sheriff Williams y notificarle de su comportamiento.
Michael: Espere, ¿Dijo Sheriff Williams? ¿Acaso viene usted de su parte?.
Richard:  Sí, ¿Necesita que lo llame desde su casa para corroborarlo o planea hacerme perder más tiempo?.
Michael: Oh, entiendo. Bueno, para que lo sepa, tengo una llave maestra para mis propiedades. Creo que está en mi cabaña, es la última de la derecha.
** [Obligarlo a cooperar]
~ Moralidad = -50
Richard: ¿Esa es su respuesta a una acusación de secuestro? Será mejor que comiences a hablar antes que haga que te tragues tus dientes.
Michael: No solo vienes a demandar información confidencial de mis inquilinos, ¿Sinó que además me insultas, amenazas y cuestionas? Después que acabe contigo te tragarás TUS propios dientes.
~Combate=true
* [Intimidarlo]
~ Moralidad = -50
Michael: No solo vienes a demandar información confidencial de mis inquilinos, ¿Sinó que además me insultas y cuestionas? Después que acabe contigo lamentarás hablarme así.
~Combate=true
